using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    float speed = 5f;
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
    }

    public void onPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currWaypoint = path[0];
        while (true)
        {
            // transform.position == currWaypoint
            // Vector3.Distance(transform.position, currWaypoint) <= 3f
            if (Vector3.Distance(transform.position, currWaypoint) <= 2.5f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

}
