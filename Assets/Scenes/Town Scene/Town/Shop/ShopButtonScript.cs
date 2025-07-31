using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonScript : MonoBehaviour
{
    public int itemID;
    public TMP_Text priceTxt;
    public TMP_Text potionNameTxt;
    public GameObject shopManager;
    public Image potionSprite;
    public Image coinSprite;

    void Update()
    {
        List<Potion> shopItems = shopManager.GetComponent<ShopManagerScript>().shopItems;

        if (itemID < shopItems.Count)
        {   
            Potion potion = shopItems[itemID];

            priceTxt.text = potion.itemPrice.ToString();
            potionNameTxt.text = potion.itemName;
            potionSprite.sprite = potion.itemImg;

            if(InventoryManager.inventoryInstance.coins < potion.itemPrice)
            {
                priceTxt.color = Color.red;
            }
            else
            {
                priceTxt.color = Color.white;
            }
            togglePlaceHolderUI(true);
        }
        else
        {
            togglePlaceHolderUI(false);
        }
        

    }

    void togglePlaceHolderUI(bool state)
    {
        priceTxt.gameObject.SetActive(state);
        potionNameTxt.gameObject.SetActive(state);
        potionSprite.gameObject.SetActive(state);
        coinSprite.gameObject.SetActive(state);
    }


}

