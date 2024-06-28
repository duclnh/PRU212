using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem
{
    public AnimalData animalData;
    public string itemName;
    public DateTime dateTime;
    public int currentStage;
    public int quantityHarvested;
    public int priceHarvested;
    public bool hungry;
    public bool sick;
    public AnimalItem(AnimalData animalData, string itemName)
    {
        this.animalData = animalData;
        this.dateTime = DateTime.UtcNow;
        this.currentStage = 0;
        this.quantityHarvested = animalData.quantity;
        this.itemName = itemName;
        this.priceHarvested = animalData.price;
        this.hungry = false;
        this.sick = false;
    }
    public override string ToString()
    {
        return $"{animalData.ToString()},ItemName: {itemName}, Datetime: {dateTime}, CurrentStage: {currentStage}, QuantityHarvested: {quantityHarvested}, PriceHarvested: {priceHarvested},Hungry: {hungry},Sick: {sick}";
    }
}
