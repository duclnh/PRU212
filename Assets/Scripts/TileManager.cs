using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;
    [SerializeField] AudioClip impactOnGroundAudio;

    void Start(){
        foreach(var position in interactableMap.cellBounds.allPositionsWithin){
            TileBase tile = interactableMap.GetTile(position);
            if(tile != null && tile.name == "Interactable_visible"){
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool IsInteractable(Vector3Int posistion){
        TileBase tile = interactableMap.GetTile(posistion);
        if(tile != null){
            if(tile.name == "Interactable"){
                return true;
            }
        }
        return false;
    }
    public void SetInteracted(Vector3Int position){
        AudioSource.PlayClipAtPoint(impactOnGroundAudio, Camera.main.transform.position);
        interactableMap.SetTile(position, interactedTile);
    }
}
