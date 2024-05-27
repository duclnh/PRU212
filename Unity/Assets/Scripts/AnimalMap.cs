using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimalMapManager : MonoBehaviour
{
    public Tilemap animalMap;
    void Start()
    {
        foreach (var position in animalMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = animalMap.GetTile(position);
        }
    }

    void Update()
    {
        
    }
}
