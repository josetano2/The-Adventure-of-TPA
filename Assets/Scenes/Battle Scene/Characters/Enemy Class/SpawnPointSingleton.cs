using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSingleton : MonoBehaviour
{
    public static GameObject[] spawnPoints;
    public static List<Transform> listOfSpawnPoints = new List<Transform>();
    public static SpawnPointSingleton spawnPointInstance;
    void Start()
    {
        if (spawnPointInstance != null && spawnPointInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        spawnPointInstance = this;
        DontDestroyOnLoad(gameObject);

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach (GameObject temp in spawnPoints)
        {
            Transform spawnPointTemp = temp.transform;
            listOfSpawnPoints.Add(spawnPointTemp);
        }
    }
}
