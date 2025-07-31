using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacker : Enemy
{
    [SerializeField] private GameObject towerAttackerPrefab;
    [SerializeField] private Transform crystal;
    private Canvas canvas;
    private Bar3DManager bar3dManager;

    void Awake()
    {
        CurrHP = HP;
        Target = getTarget();
        canvas = GetComponentInChildren<Canvas>();
        bar3dManager = FindObjectOfType<Bar3DManager>();
        bar3dManager.setMaxHealth(HP);
        bar3dManager.setHealth(HP);
    }

    void Update()
    {
        canvas.transform.position = transform.position + Vector3.up * 2f;
        bar3dManager.setHealth(CurrHP);
    }

    public override void spawnEnemy()
    {
        int min = 0;
        int max = 3;
        int randNum;
        randNum = Random.Range(min, max);
        EnemyGameObject = Instantiate(towerAttackerPrefab, SpawnPointSingleton.listOfSpawnPoints[randNum].position, SpawnPointSingleton.listOfSpawnPoints[randNum].rotation);
    }

    public override Transform getTarget()
    {
        crystal = GameObject.Find("Crystal Object").transform;
        return crystal;
    }
}
