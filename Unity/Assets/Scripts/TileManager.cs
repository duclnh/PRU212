using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap interactableMap;
    public Tile hiddenInteractableTile;
    public Tile plowedTile;


    void Start()
    {
        if (interactableMap != null)
        {
            foreach (var position in interactableMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null && tile.name == "Interactable_visible")
                {
                    interactableMap.SetTile(position, hiddenInteractableTile);
                }
            }
        }
    }

    public void SetInteracted(Vector3Int position)
    {
        GameManager.instance.menuSettings.SoundImpactGround();
        interactableMap.SetTile(position, plowedTile);
    }
    public void RestoreIntered(Vector3Int position)
    {
        interactableMap.SetTile(position, hiddenInteractableTile);
    }
    public string GetTileName(Vector3Int position)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null)
            {
                return tile.name;
            }
        }
        return "";
    }

}
