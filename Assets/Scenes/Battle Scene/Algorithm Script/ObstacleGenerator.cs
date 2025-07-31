using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private BSP bspGen;
    [SerializeField] private Prim primGen;
    [SerializeField] private GameObject obstaclePrefab;
    void Start()
    {
        
        foreach (BoundsInt room in bspGen.Rooms)
        {
            if (checkCollisionFromPrim(room))
            {
                if (UnityEngine.Random.value < 2)
                {
                    Debug.Log("masuk");
                    spawnObstacle(room);
                }
            }
        }

    }

    public bool checkCollisionFromPrim(BoundsInt room)
    {
        for(int i = room.min.x; i < room.max.x; i++)
        {
            for(int j = room.min.z; j < room.max.z; j++)
            {
                if(primGen.Map[i, j] != ' ')
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void spawnObstacle(BoundsInt room)
    {
        Vector3 pos = new Vector3(room.center.x, 0f, room.center.z);
        float spawnRange = 5f;

        bool canSpawn = true;

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, spawnRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.gameObject.CompareTag("SpawnPoint") || collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Crystal") || collider.gameObject.CompareTag("Platform"))
            {
                canSpawn = false;
                break;
            }
        }
        if (canSpawn)
        {
            Instantiate(obstaclePrefab, pos + Vector3.up * 11f, Quaternion.identity);
        }
    }
}
