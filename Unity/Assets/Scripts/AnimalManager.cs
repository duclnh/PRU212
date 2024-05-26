using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimalManager : MonoBehaviour
{
    public Tilemap animalMap;
    private List<Vector3> positionAnimalMap = new List<Vector3>();
    public GameObject chickenObject;
    public GameObject cowObject;
    public AnimalData chickenData;
    public AnimalData cowData;

    private Dictionary<GameObject, AnimalItem> animalDataDictionary = new Dictionary<GameObject, AnimalItem>();
    void Start()
    {
        if (animalMap != null)
        {
            foreach (var position in animalMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = animalMap.GetTile(position);
                if (tile != null && tile.name == "Interactable_visible")
                {
                    positionAnimalMap.Add(position);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<GameObject, AnimalItem> objectAnimal in animalDataDictionary)
        {
            System.TimeSpan timeDifference = objectAnimal.Value.dateTime - System.DateTime.UtcNow;
            if (timeDifference.TotalSeconds <= 0 && objectAnimal.Value.currentStage <= objectAnimal.Value.animalData.numberStage)
            {
                objectAnimal.Value.currentStage++;
                objectAnimal.Key.transform.localScale = new Vector3(CheckDirection(objectAnimal.Key.transform.localScale.x, 0.5F),
                                                                    objectAnimal.Key.transform.localScale.y + 0.5F,
                                                                    objectAnimal.Key.transform.localScale.z);
                if (objectAnimal.Value.currentStage < objectAnimal.Value.animalData.numberStage)
                {
                    objectAnimal.Value.dateTime = objectAnimal.Value.dateTime.AddSeconds(objectAnimal.Value.animalData.growTime);
                }
                else
                {
                    Vector3Int positionInt = new Vector3Int(
                            Mathf.RoundToInt(objectAnimal.Key.transform.position.x),
                            Mathf.RoundToInt(objectAnimal.Key.transform.position.y),
                            Mathf.RoundToInt(objectAnimal.Key.transform.position.z)
                        );
                    GameManager.instance.menuSettings.SoundComplete(positionInt);
                }
            }
        }
    }

    private float CheckDirection(float width, float change)
    {
        if (width < 0)
        { 
            return width - change;
        }
        return width + change;
    }
    private Vector3 GetRandomPosistion()
    {
        return positionAnimalMap[Random.Range(0, positionAnimalMap.Count - 1)];
    }
    private GameObject GetPrefab(string name)
    {
        return name == "Chicken Breed" ? chickenObject : cowObject;
    }
    public AnimalItem GetAnimalData(string name)
    {
        AnimalData data = name == "Chicken Breed" ? chickenData : cowData;
        return new AnimalItem(data, name);

    }
    public void DropAnimal(string itemName)
    {
        Vector3 spawnPosistion = GetRandomPosistion();
        GameObject animal = Instantiate(GetPrefab(itemName), spawnPosistion, Quaternion.identity, transform);
        animalDataDictionary.Add(animal, GetAnimalData(itemName));
    }
}
