using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Slot_UI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public Button buttonRemove;
    public void SetItem(Inventory.Slot slot){
        if (slot != null){
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1,1,1,1);
            quantityText.text = slot.count.ToString();
            buttonRemove.gameObject.SetActive(false);
        }
        buttonRemove.gameObject.SetActive(true);
    } 
    public void SetEmpty(){
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
        quantityText.text ="";
        buttonRemove.gameObject.SetActive(false);
    }
}
