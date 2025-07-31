using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    //public static ShopManagerScript shopInstance { get; private set; }
    public List<Potion> shopItems;
    public TMP_Text coinsTxt;
    public TMP_Text slotsTxt;

    void Start()
    {
        shopItems = new List<Potion>();

        coinsTxt.text = InventoryManager.inventoryInstance.coins.ToString();
        Debug.Log(InventoryManager.inventoryInstance.coins);
        slotsTxt.text = InventoryManager.inventoryInstance.slots.ToString() + "/" + InventoryManager.inventoryInstance.maxSlots.ToString();

        if (PotionManager.potionInstance.hpPotionPrefab != null && !isPotionAdded(PotionManager.potionInstance.hpPotionPrefab))
        {
            Potion hpPotion = PotionManager.potionInstance.hpPotionPrefab.GetComponent<Potion>();
            shopItems.Add(hpPotion);
        }

        // Add Mana Potion
        if (PotionManager.potionInstance.manaPotionPrefab != null && !isPotionAdded(PotionManager.potionInstance.manaPotionPrefab))
        {
            Potion manaPotion = PotionManager.potionInstance.manaPotionPrefab.GetComponent<Potion>();
            shopItems.Add(manaPotion);
        }



        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].itemID = i;
        }
    }

    bool isPotionAdded(GameObject potionPrefab)
    {
        foreach (Potion potion in shopItems)
        {
            if (potionPrefab.GetComponent<Potion>().itemName == potion.itemName)
            {
                return true;
            }
        }
        return false;
    }
    public void buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        Debug.Log(ButtonRef);
        int itemID = ButtonRef.GetComponent<ShopButtonScript>().itemID;
        if (itemID >= shopItems.Count || itemID < 0)
        {
            return;
        }
        Potion selectedPotion = shopItems[itemID];

        //if (coins >= selectedPotion.itemPrice && slots < maxSlots)
        //{
        //    coins -= selectedPotion.itemPrice;
        //    //shopItems[3, ButtonRef.GetComponent<ShopButtonScript>().itemID]++;
        //    coinsTxt.text = coins.ToString();
        //    slots++;
        //    slotsTxt.text = slots.ToString() + "/" + maxSlots.ToString();
        //}

        if (addItemToInventory(selectedPotion))
        {
            coinsTxt.text = InventoryManager.inventoryInstance.coins.ToString();
            slotsTxt.text = InventoryManager.inventoryInstance.slots.ToString() + "/" + InventoryManager.inventoryInstance.maxSlots.ToString();

        }
    }

    public bool addItemToInventory(Potion potion)
    {
        if (InventoryManager.inventoryInstance.coins >= potion.itemPrice && InventoryManager.inventoryInstance.slots <= InventoryManager.inventoryInstance.maxSlots)
        {
            Debug.Log("Beli");
            InventoryManager.inventoryInstance.coins -= potion.itemPrice;
            InventoryManager.inventoryInstance.slots++;
            InventoryManager.inventoryInstance.inventoryPotions.Add(potion);
            return true;
        }
        else
        {
            return false;
        }
    }
}
