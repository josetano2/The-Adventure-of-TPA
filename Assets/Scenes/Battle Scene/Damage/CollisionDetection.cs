using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private EnemyManager enemyManager;
    public CrystalManager crystal;
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && playerManager.ActiveController.IsAttacking)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Debug.Log(enemy);
            playerManager.ActiveController.IsAttacking = false;

            if (!playerManager.ActiveController.IsHeavy)
            {
                enemy.takeDamage(playerManager.ActivePlayer.LeftClickDamage);
                Debug.Log(enemy.CurrHP);
            }
            else
            {
                enemy.takeDamage(playerManager.ActivePlayer.RightClickDamage);
            }
        }


    }


}
