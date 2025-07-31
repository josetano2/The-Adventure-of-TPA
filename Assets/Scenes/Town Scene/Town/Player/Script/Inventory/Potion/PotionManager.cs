using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager potionInstance { get; private set; }

    public GameObject hpPotionPrefab;
    public GameObject manaPotionPrefab;

    void Start()
    {
        if(potionInstance != null && potionInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        potionInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
