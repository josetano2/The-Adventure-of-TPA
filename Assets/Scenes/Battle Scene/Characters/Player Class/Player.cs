using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float currHP;
    [SerializeField] private float mana = 50;
    [SerializeField] private float currMana;
    [SerializeField] private float leftClickDamage;
    [SerializeField] private float rightClickDamage;
    private GameObject enemyGameObject;
    private Transform target;

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float CurrHP
    {
        get { return currHP; }
        set { currHP = value; }
    }
    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }
    public float CurrMana
    {
        get { return currMana; }
        set { currMana = value; }
    }
    public float LeftClickDamage
    {
        get { return leftClickDamage; }
        set { leftClickDamage = value; }
    }
    public float RightClickDamage
    {
        get { return rightClickDamage; }
        set { rightClickDamage = value; }
    }

    public void takeDamage(float damage)
    {
        currHP -= damage;
        
    }
    public void removePlayer()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public Transform getEnemyTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            Transform nearestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, nearestEnemy.position);

            for (int i = 1; i < enemies.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemies[i].transform;
                }
            }

            target = nearestEnemy;
            return target;
        }
        return null;
    }
}
