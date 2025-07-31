using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager inventoryInstance { get; private set; }

    public float coins;
    public int slots;
    public int maxSlots = 8;
    public List<Potion> inventoryPotions;

    void Awake()
    {
        if (inventoryInstance != null && inventoryInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        inventoryInstance = this;
        DontDestroyOnLoad(gameObject);
        inventoryPotions = new List<Potion>();

    }

    public void addCoins(float newCoins)
    {
        coins += newCoins;
        Debug.Log(coins);
    }

}
