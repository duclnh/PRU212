using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] List<Slot_UI> slots = new List<Slot_UI>();

    void Start()
    {
        Instantiate(inventory);
        inventory.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
            Refresh();
        }
    }

    public void ToggleInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
            Refresh();
        }
        else
        {
            inventory.SetActive(false);
        }
    }
    void Refresh()
    {
        if (slots.Count == playerMovement.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (playerMovement.inventory.slots[i].type != CollectionType.NONE)
                {
                    slots[i].SetItem(playerMovement.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotID)
    {
        CollecTable itemToDrop = GameManager.instance.itemManager.GetItemByType(playerMovement.inventory.slots[slotID].type);
        if(itemToDrop != null){
            playerMovement.DropItem(itemToDrop);
            playerMovement.inventory.Remove(slotID);
            Refresh();
        }
    }

}
