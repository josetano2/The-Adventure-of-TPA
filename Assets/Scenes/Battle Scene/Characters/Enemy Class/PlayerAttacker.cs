using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Enemy
{
    [SerializeField] private GameObject playerAttackerPrefab;
    private List<Transform> players = new List<Transform>();
    private Canvas canvas;
    private Bar3DManager bar3dManager;
    void Awake()
    {
        CurrHP = HP;
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
        GameObject[] playersTemp = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject temp in playersTemp)
        {
            Transform playerTransform = temp.transform;
            //Debug.Log(temp);
            players.Add(playerTransform);
        }

        int min = 0;
        int max = 3;
        int randNum;
        randNum = Random.Range(min, max);
        EnemyGameObject = Instantiate(playerAttackerPrefab, SpawnPointSingleton.listOfSpawnPoints[randNum].position, SpawnPointSingleton.listOfSpawnPoints[randNum].rotation);
    }

    public override Transform getTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length > 0)
        {
            Transform nearestPlayer = players[0].transform;
            float closestDistance = Vector3.Distance(transform.position, nearestPlayer.position);

            for (int i = 1; i < players.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, players[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestPlayer = players[i].transform;
                }
            }

            Target = nearestPlayer;
            return Target;
        }
        return null;
    }
}
