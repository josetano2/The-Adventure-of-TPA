using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnemy : MonoBehaviour
{
    public Transform target;
    float speed = 5f;
    Vector3[] path;
    int targetIndex;
    private Animator animator;
    private bool isOnPath = true;
    public Animator AnimatorPlayer
    {
        get { return animator; }
        set { animator = value; }
    }

    public TowerAttacker towerAttacker;

    void Start()
    {
        animator = GetComponent<Animator>();
        towerAttacker = GetComponent<TowerAttacker>();
        target = towerAttacker.Target;
        PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 4f && !isOnPath)
        {
            animator.SetBool("isAttacking", false);
            PathRequestManager.RequestPath(transform.position, target.position, onPathFound);
            isOnPath = true;
        }
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * speed);
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
        Vector3 currWaypoint = path[0];
        while (true)
        {
            // transform.position == currWaypoint
            // Vector3.Distance(transform.position, currWaypoint) <= 3f
            if (Vector3.Distance(transform.position, currWaypoint) <= 3f)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    isOnPath = false;
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
