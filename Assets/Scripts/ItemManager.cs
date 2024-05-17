using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public CollecTable [] collecTablesItems;
    private Dictionary<CollectionType, CollecTable> collecTableItemsDict = new Dictionary<CollectionType, CollecTable>();

    private void Awake(){
        foreach(CollecTable item in  collecTablesItems){
            AddItem(item);
        }
       
    }
    private void AddItem(CollecTable item){
        if(!collecTableItemsDict.ContainsKey(item.type)){
            collecTableItemsDict.Add(item.type, item);
        }
    }
    public CollecTable GetItemByType(CollectionType type){
        if(collecTableItemsDict.ContainsKey(type)){
            return collecTableItemsDict[type];
        }
        return null;
    }
}
