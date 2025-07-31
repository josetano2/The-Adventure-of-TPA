using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionTowerAttacker : MonoBehaviour
{

    private CrystalManager crystal;
     private PlayerManager playerManager;
    [SerializeField] private TowerAttacker towerAttacker;

    private bool canAttack = true;

    void Start()
    {
        crystal = FindObjectOfType<CrystalManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.CompareTag("Crystal") && canAttack)
        //{
        //    crystal.takeDamage(towerAttacker.Damage);
        //    canAttack = false;
        //    StartCoroutine(resetAttackCooldown());
        //}
        //if (collision.gameObject.CompareTag("Player") && canAttack)
        //{
        //    Player player = collision.gameObject.GetComponent<Player>();
        //    if (player != null)
        //    {
        //        int playerIndex = playerManager.getPlayerIndex(player);
        //        if (playerIndex != -1)
        //        {
        //            playerManager.listOfPlayer[playerIndex].takeDamage(towerAttacker.Damage);
        //            canAttack = false;
        //            StartCoroutine(resetAttackCooldown());
        //        }
        //    }
        //}

        if (canAttack && (collision.gameObject.CompareTag("Crystal") || collision.gameObject.CompareTag("Player")))
        {
            if (collision.gameObject.CompareTag("Crystal"))
            {
                crystal.takeDamage(towerAttacker.Damage);
            }
            if (collision.gameObject.CompareTag("Player") && canAttack)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    int playerIndex = playerManager.getPlayerIndex(player);
                    if (playerIndex != -1)
                    {
                        playerManager.listOfPlayer[playerIndex].takeDamage(towerAttacker.Damage);
                    }
                }
            }
            canAttack = false;
            StartCoroutine(resetAttackCooldown());
        }
    }

    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
