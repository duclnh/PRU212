using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();
    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        backpack = new Inventory(backpackSlotCount);
        toolbar = new Inventory(toolbarSlotCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }
    public void Add(string inventoryName, Item item)
    {
        if (inventoryName != null)
        {
            if (inventoryByName.ContainsKey(inventoryName))
            {
                inventoryByName[inventoryName].Add(item);
            }
        }
    }
    
    public void Add(string inventoryName, Item item, int slotId)
    {
        if (inventoryName != null)
        {
            if (inventoryByName.ContainsKey(inventoryName))
            {
                inventoryByName[inventoryName].Add(item, slotId);
            }
        }
    }
    public bool Remove(string inventoryName, string itemName)
    {
        if (inventoryName != null)
        {
            if (inventoryByName.ContainsKey(inventoryName))
            {
                return inventoryByName[inventoryName].Remove(itemName);
            }
        }
        return false;
    }
    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null;
    }
}
