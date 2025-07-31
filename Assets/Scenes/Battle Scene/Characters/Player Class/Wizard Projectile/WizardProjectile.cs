using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{
    private BattleMovementController con;
    private PlayerManager playerManager;
    [SerializeField] private Camera cam;
    private Vector3 destination;
    private float projectileSpeed = 20f;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform staff;

    void Start()
    {
        con = FindAnyObjectByType<BattleMovementController>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }
    void Update()
    {
        if(con.canAttack && !con.IsMoving && playerManager.ActiveController == playerManager.AraszkiewiczController)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                shootProjectile();
            }
        }
    }

    void shootProjectile()
    {

        destination = transform.position + transform.forward * 1000;
        instantiateProjectile(staff);
    }

    void instantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        Vector3 direction = destination - firePoint.position;
        projectileObj.GetComponent<Rigidbody>().velocity = direction.normalized * projectileSpeed;
    }
}
