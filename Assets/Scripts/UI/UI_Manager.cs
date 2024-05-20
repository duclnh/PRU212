using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string , Inventory_UI> inventoryUIBYName = new Dictionary<string , Inventory_UI>();
    public List<Inventory_UI> inventoryUIs;
    public GameObject inventoryPannel;
    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    private void Awake(){
        Initialize();
    }


     
    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B)){
            ToggleInventoryUI();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = false;
        }
        else
        {
            dragSingle = true;
        }
    }
    
    public void ToggleInventoryUI()
    {
        if (inventoryPannel != null)
        {
            if (!inventoryPannel.activeSelf)
            {
                inventoryPannel.SetActive(true);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPannel.SetActive(false);
            }
        }
    }
    public void RefreshInventoryUI(string inventoryName){
        if (inventoryUIBYName.ContainsKey(inventoryName)){
            inventoryUIBYName[inventoryName].Refresh();
        }
    }
    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIBYName){
            keyValuePair.Value.Refresh();
        }
    }
    private void Initialize()
    {
        foreach (Inventory_UI ui in inventoryUIs){
            if(!inventoryUIBYName.ContainsKey(ui.inventoryName)){
                inventoryUIBYName.Add(ui.inventoryName, ui);
            }
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName){
        if(inventoryUIBYName.ContainsKey(inventoryName)){
            return inventoryUIBYName[inventoryName];
        }
        return null;
    }
}
