using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Crop Data", menuName = "Crop Data", order = 50)]
public class CropData : ScriptableObject
{
    [SerializeField] public string nameItem = "";
    [SerializeField] public float growTime = 3f;
    [SerializeField] public List<Tile> tiles = new List<Tile>();
    [SerializeField] public int quantity = 10;

    private string toStringTile()
    {
        string result = "";
        foreach (Tile tile in tiles)
        {
            if (tile != null)
            {
                result += $"{AssetDatabase.GetAssetPath(tile)} ";
            }
        }
        return result;
    }
    public override string ToString()
    {
        return $"Crop: {nameItem}, Grow Time: {growTime} sec, Quantity: {quantity}, Tiles: {toStringTile()}";
    }
}
