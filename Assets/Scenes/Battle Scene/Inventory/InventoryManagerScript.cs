using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManagerScript : MonoBehaviour
{
    public List<Potion> inventoryPotions;

    void Update()
    {
        inventoryPotions = InventoryManager.inventoryInstance.inventoryPotions;
    }

    public void useItem()
    {
        Debug.Log("tes");
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        Debug.Log(ButtonRef);
        int inventoryID = ButtonRef.GetComponent<InventoryButtonScript>().inventoryID;
        if (inventoryID >= 0 && inventoryID < InventoryManager.inventoryInstance.inventoryPotions.Count)
        {
            Potion selectedPotion = InventoryManager.inventoryInstance.inventoryPotions[inventoryID];
            if (selectedPotion.validateStat())
            {
                selectedPotion.potionBuff();
                InventoryManager.inventoryInstance.inventoryPotions.RemoveAt(inventoryID);
                InventoryManager.inventoryInstance.slots--;

            }
        }
    }
}
