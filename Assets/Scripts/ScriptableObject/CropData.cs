using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Crop Data", menuName = "Crop Data", order = 50)]
public class CropData : ScriptableObject
{
    [SerializeField] public string nameItem = "";
    [SerializeField] public float growTime = 3f;
    [SerializeField] public int currentStage = 0;
    [SerializeField] public int numberStage = 4;
    [SerializeField] public List<Tile> tiles = new List<Tile>();
    [SerializeField] public int quantity = 10;
}
