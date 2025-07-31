using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Potion : MonoBehaviour
{
    public int itemID;
    public string itemName; 
    public int itemPrice;
    public Sprite itemImg;
    public PlayerManager playerManager;
   

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    public Potion(int id, string name, int price, Sprite img)
    {
        itemID = id;
        itemName = name;
        itemPrice = price;
        itemImg = img;
    }

    public abstract void potionBuff();
    public abstract bool validateStat();
}
