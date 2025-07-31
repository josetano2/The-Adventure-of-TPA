using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float currHP;
    [SerializeField] private float damage;
    private Transform target;
    private GameObject enemyGameObject;

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
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }
    public GameObject EnemyGameObject
    {
        get { return enemyGameObject; }
        set { enemyGameObject = value; }
    }

    public void takeDamage(float damage)
    {
        currHP -= damage;
        if (currHP <= 0)
        {
            removeEnemy();
        }
    }

    public void removeEnemy()
    {
        Destroy(gameObject);
        EnemyManager.counter--;
        TimerScript.enemyKillCount++;
    }

    public abstract void spawnEnemy();
    public abstract Transform getTarget();

}
