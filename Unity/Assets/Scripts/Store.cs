using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject storePannel;
    [SerializeField] List<Item> storeItems = new List<Item>();
    private MenuSettings menuSettings;

    public Dictionary<string, Item> keyValuePairs = new Dictionary<string, Item>();
    void Start()
    {
        if (storeItems != null)
        {
            foreach (var item in storeItems)
            {
                if (!keyValuePairs.ContainsKey(item.data.itemName))
                {
                    keyValuePairs.Add(item.data.itemName, item);
                }
            }
        }
        ToggleStore();
        menuSettings = GameManager.instance.menuSettings;
    }
    public Sprite GeticonFromStore(string itemName){
        if(keyValuePairs.ContainsKey((itemName))){
            return keyValuePairs[itemName].data.icon;
        }
        return null;
    }
    public void ToggleStore()
    {
        if (storePannel != null)
        {
            if (!storePannel.activeSelf)
            {
                storePannel.SetActive(true);
            }
            else
            {
                storePannel.SetActive(false);
            }
        }
    }

    public void BuyItem(int indexItem)
    {
        if (indexItem < 0 || indexItem >= storeItems.Count)
        {
            Debug.LogError("Invalid item index");
            return;
        }

        if (storeItems[indexItem].data.itemName.Contains("Breed"))
        {
            if (GameManager.instance.player.BuyItemStore(storeItems[indexItem].data.price))
            {
                GameManager.instance.animalManager.DropAnimal(storeItems[indexItem].data.itemName);
            }
            else
            {
                menuSettings.SoundFail();
                GameManager.instance.nofification.Show("You not enough money");
            }
        }
        else
        {
            Item newItem = new Item();
            newItem.data = new ItemData();
            newItem.data.itemName = storeItems[indexItem].data.itemName;
            newItem.data.icon = storeItems[indexItem].data.icon;
            if (GameManager.instance.player.BuyItemStore(storeItems[indexItem].data.price))
            {
                GameManager.instance.player.inventoryManager.Add("Backpack", newItem);
                GameManager.instance.uiManager.RefreshAll();
            }
            else
            {
                menuSettings.SoundFail();
                GameManager.instance.nofification.Show("You not enough money");
            }
        }
    }



    public void SellItem(int indexItem)
    {
        if (indexItem < 0 || indexItem >= storeItems.Count)
        {
            Debug.LogError("Invalid item index");
            return;
        }

        if (GameManager.instance.player.inventoryManager.toolbar.GetSelectSlot().itemName != storeItems[indexItem].name)
        {
            menuSettings.SoundFail();
            GameManager.instance.nofification.Show("Please choose item need sell");
            return;
        }
        bool result = GameManager.instance.player.inventoryManager.Remove("Toolbar", storeItems[indexItem].name);
        if (result)
        {
            GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
            GameManager.instance.player.SellItemStore(storeItems[indexItem].data.price);
        }
        else
        {
            menuSettings.SoundFail();
            GameManager.instance.nofification.Show("You don't have this item");
        }
    }

    public bool ToggleStoreStatus()
    {
        if (storePannel != null)
        {
            return storePannel.activeSelf;
        }
        return false;
    }
}
