using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetEnemy : MonoBehaviour
{
    private Transform target;
    float speed = 5f;
    Vector3[] path;
    int targetIndex;
    private Animator animator;
    private float requestInterval = 0f;
    public Animator AnimatorPlayer
    {
        get { return animator; }
        set { animator = value; }
    }

    public PlayerAttacker playerAttacker;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttacker = GetComponent<PlayerAttacker>();
    }

    void Update()
    {
        target = playerAttacker.getTarget();
        if(target != null)
        {
            if ((Vector3.Distance(transform.position, target.position) > 0.5f) && Time.time - requestInterval > 1f)
            {
                requestInterval = Time.time + 1f;
                animator.SetBool("isAttacking", false);
                PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
                
            }
        }
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
            if (Vector3.Distance(transform.position, currWaypoint) <= 0.4f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    animator.SetBool("isAttacking", true);
                    yield break;
                }
                currWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
