using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TowerAttacker towerAttacker;
    [SerializeField] private PlayerAttacker playerAttacker;
    int randNum;
    public static int counter = 3;
    void Start()
    {
        playerAttacker.spawnEnemy();
        towerAttacker.spawnEnemy();
        towerAttacker.spawnEnemy();
        StartCoroutine(spawnAllEnemy());
    }
    IEnumerator spawnAllEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            spawnRandomEnemy();
        }
    }

    void spawnRandomEnemy()
    {
        randNum = Random.Range(0, 2);
        if(counter < 6)
        {
            if (randNum == 0)
            {
                towerAttacker.spawnEnemy();
                counter++;
            }
            else
            {
                playerAttacker.spawnEnemy();
                counter++;
            }
        }
    }

}
