using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropItem
{
    public CropData cropData;
    public string itemName;
    public DateTime dateTime;
    public int currentStage;
    public int quantityHarvested;
    public CropItem(CropData cropData, string itemName){
        this.cropData = cropData;
        this.dateTime = DateTime.UtcNow;
        this.currentStage = 0;
        this.quantityHarvested = cropData.quantity;
        this.itemName = itemName;
    }
    public override string ToString()
    {
        return $"Itemname: {itemName} ,Datetime: {dateTime}, CurrentStage: {currentStage}, QuantityHarvested: {quantityHarvested}, CropDate: {cropData.ToString()}";
    }
}
