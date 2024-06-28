using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal Data", menuName = "Animal Data", order = 50)]
public class AnimalData : ScriptableObject
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public string nameItem = "";
    [SerializeField] public float growTime = 3f;
    [SerializeField] public int numberStage = 4;
    [SerializeField] public int price = 500;
    [SerializeField] public int quantity = 5;

    public override string ToString()
    {
        return $"MoveSpeed: {moveSpeed}, NameItem: {nameItem}, GrowTime: {growTime}, NumberStage: {numberStage},Price: {price}, Quantity: {quantity}";
    }
}
