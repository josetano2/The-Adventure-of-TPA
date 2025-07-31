using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : MonoBehaviour
{
    private Transform target;
    float speed = 5f;
    Vector3[] path;
    int targetIndex;
    private Animator animator;
    private bool isOnPath = false;
    private float requestInterval = 0f;
    public Animator AnimatorPlayer
    {
        get { return animator; }
        set { animator = value; }
    }

    public Player player;

    void Start()
    {
        //animator = GetComponent<Animator>();
        //playerAttacker = GetComponent<PlayerAttacker>();
    }

    void Update()
    {
        target = player.getEnemyTarget();
        if (target != null)
        {
            if ((Vector3.Distance(transform.position, target.position) > 1f || !isOnPath) && Time.time - requestInterval > 0.4f)
            {
                requestInterval = Time.time + 0.4f;
                //animator.SetBool("isAttacking", false);
                PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
                isOnPath = true;
            }
            //if (!animator.GetBool("isAttacking"))
            //{
            //    Vector3 direction = (target.position - transform.position).normalized;
            //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
            //}
        }
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
        targetIndex = 0;
        if (path.Length == 0)
        {
            yield break;
        }
        Vector3 currWaypoint = path[0];
        while (true)
        {
            if (Vector3.Distance(transform.position, currWaypoint) <= 0.5f)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    isOnPath = false;
                    //animator.SetBool("isAttacking", true);
                    yield break;
                }
                currWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
