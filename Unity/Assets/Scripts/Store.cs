using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject storePannel;
    [SerializeField] List<Item> storeItems = new List<Item>();
    private MenuSettings menuSettings;
    void Start()
    {
        ToogleStore();
        menuSettings = GameManager.instance.menuSettings;
    }
    public void ToogleStore()
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
                GameManager.instance.nofification.Show("-" + storeItems[indexItem].data.price);
                menuSettings.SoundBuyItem();
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
                GameManager.instance.nofification.Show("-" + storeItems[indexItem].data.price);
                GameManager.instance.uiManager.RefreshAll();
                menuSettings.SoundBuyItem();
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
        bool reslut = GameManager.instance.player.inventoryManager.Remove("Toolbar", storeItems[indexItem].name);
        if (reslut)
        {
            menuSettings.SoundSellItem();
            GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
            GameManager.instance.player.SellItemStore(storeItems[indexItem].data.price);
            GameManager.instance.nofification.Show("+" + storeItems[indexItem].data.price);
        }
        else
        {
            menuSettings.SoundFail();
            GameManager.instance.nofification.Show("You don't have this item");
        }
    }

    public bool ToggleStoreStatus() => storePannel.activeSelf;
}
