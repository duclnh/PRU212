using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManger : MonoBehaviour
{
    public Tilemap cropMap;
    public Tile hiddenInteractableTile;
    public List<CropData> cropDatas = new List<CropData>();
    [SerializeField] AudioClip impactOnGroundAudio;

    private Dictionary<Vector3Int, CropData> cropDataDictionary = new Dictionary<Vector3Int, CropData>();

    void Start()
    {
        foreach (var position in cropMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = cropMap.GetTile(position);
            if (tile != null && tile.name == "Interactable_visible")
            {
                cropMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }
    void Update()
    {
        foreach (var position in cropDataDictionary)
        {

        }
    }
    public void Seed(Vector3Int position, string nameSeed)
    {
        Debug.Log(nameSeed);
        AudioSource.PlayClipAtPoint(impactOnGroundAudio, Camera.main.transform.position);
        CropData cropData = GetCropDataByName(nameSeed);
        cropDataDictionary.Add(position, cropData);
        cropMap.SetTile(position, cropData.tiles[0]);
        GameManager.instance.player.inventoryManager.Remove("Toolbar", nameSeed);
        GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
    }
    public string GetTileName(Vector3Int position)
    {
        if (cropMap != null)
        {
            TileBase tile = cropMap.GetTile(position);
            if (tile != null)
            {
                return tile.name;
            }
        }
        return "";
    }
    public CropData GetCropDataByName(string name) => cropDatas.Find(x => x.name == name);
}
