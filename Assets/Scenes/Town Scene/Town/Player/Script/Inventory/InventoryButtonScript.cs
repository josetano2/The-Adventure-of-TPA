using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButtonScript : MonoBehaviour
{
    public int inventoryID;
    public TMP_Text potionNameTxt;
    public InventoryManager inventoryManager;
    public Image potionSprite;

    void Start()
    {
        inventoryManager = InventoryManager.inventoryInstance;
    }

    void Update()
    {

        List<Potion> inventoryItems = InventoryManager.inventoryInstance.inventoryPotions;
        if (inventoryID < inventoryItems.Count)
        {
            Potion inventory = inventoryItems[inventoryID];
            potionNameTxt.text = inventory.itemName;
            potionSprite.sprite = inventory.itemImg;
            togglePlaceHolderUI(true);
        }
        else
        {
            togglePlaceHolderUI(false);
        }
    }

    void togglePlaceHolderUI(bool state)
    {
        potionNameTxt.gameObject.SetActive(state);
        potionSprite.gameObject.SetActive(state);
    }

}
