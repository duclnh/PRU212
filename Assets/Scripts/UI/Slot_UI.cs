using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Slot_UI : MonoBehaviour
{
    public int slotID;
    public Image itemIcon;
    public Inventory inventory; 
    public TextMeshProUGUI quantityText;
    public Image imageBackground;
    public void SetItem(Inventory.Slot slot){
        if (slot != null){
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1,1,1,1);
            quantityText.text = slot.count.ToString();
        }
    } 
    public void SetEmpty(){
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
        quantityText.text = "";
    }

    public void SetSelectItem(bool status){
        if(status){
            imageBackground.color = Color.red;
        }else{
            imageBackground.color = Color.white;
        }
    }
}
