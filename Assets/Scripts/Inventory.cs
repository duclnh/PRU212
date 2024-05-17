using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    [System.Serializable]
    public class Slot{
        public CollectionType type;
        public Sprite icon;
        public int count;
        public int maxAllowed;
        public Slot(){
            type = CollectionType.NONE;
            count = 0;
            maxAllowed = 99;
        }
        public bool CanAddItem(){
            if(count < maxAllowed){
                return true;
            }
            return false;
        }
        public void AddItem(CollecTable item){
            this.type = item.type;
            this.icon = item.icon;
            count++;
        }
        public void RemoveItem(){
            if(count > 0){
                count--;
                if(count ==0){
                    icon = null;
                    type = CollectionType.NONE;
                }
            }
        }
    }
    public List<Slot> slots = new List<Slot>();
    public Inventory(int numSlots){
        for(int i = 0; i < numSlots; i++){
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    public void Add(CollecTable item){
        foreach(Slot slot in slots){
            if(slot.type == item.type && slot.CanAddItem()){
                slot.AddItem(item);
                return;
            }
        }
         foreach(Slot slot in slots){
            if(slot.type == CollectionType.NONE){
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(int index){
        slots[index].RemoveItem();
        Refresh();
    }

    private void Refresh()
    {
        
    }
}


