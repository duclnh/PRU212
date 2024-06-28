using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class AnimalManager : MonoBehaviour
{
    public Tilemap animalMap;
    private List<Vector3> positionAnimalMap = new List<Vector3>();
    public GameObject chickenObject;
    public GameObject cowObject;
    public AnimalData chickenData;
    public AnimalData cowData;

    public Dictionary<GameObject, AnimalItem> animalDataDictionary = new Dictionary<GameObject, AnimalItem>();
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

    void Update()
    {
        foreach (KeyValuePair<GameObject, AnimalItem> objectAnimal in animalDataDictionary)
        {
            System.TimeSpan timeDifference = objectAnimal.Value.dateTime - System.DateTime.UtcNow;
            if (Mathf.RoundToInt((float)timeDifference.TotalSeconds) == Random.Range(0, 2500))
            {
                Transform notificationObject = objectAnimal.Key.transform.Find("hungry");

                if (notificationObject != null && Random.Range(0F, 1F) < 0.1)
                {
                    objectAnimal.Value.hungry = true;
                    notificationObject.gameObject.SetActive(true);
                }
            }
            if (Mathf.RoundToInt((float)timeDifference.TotalSeconds) == Random.Range(0, 3000))
            {
                Transform notificationObject = objectAnimal.Key.transform.Find("sick");
                if (notificationObject != null && Random.Range(0F, 1F) < 0.07)
                {
                    objectAnimal.Value.sick = true;
                    notificationObject.gameObject.SetActive(true);
                }
            }

            if (timeDifference.TotalSeconds <= 0 && objectAnimal.Value.currentStage <= objectAnimal.Value.animalData.numberStage)
            {
                if (objectAnimal.Value.hungry)
                {
                    objectAnimal.Value.priceHarvested -= 10;
                }
                if (objectAnimal.Value.sick)
                {
                    objectAnimal.Value.priceHarvested -= 15;
                }
                objectAnimal.Value.currentStage++;
                if (objectAnimal.Key.name.Contains("Cow"))
                {
                    objectAnimal.Key.transform.localScale = new Vector3(CheckDirection(objectAnimal.Key.transform.localScale.x, 0.5F),
                                                                  objectAnimal.Key.transform.localScale.y + 0.5F,
                                                                  objectAnimal.Key.transform.localScale.z);
                }
                else
                {
                    objectAnimal.Key.transform.localScale = new Vector3(CheckDirection(objectAnimal.Key.transform.localScale.x, 0.3F),
                                                                 objectAnimal.Key.transform.localScale.y + 0.3F,
                                                                 objectAnimal.Key.transform.localScale.z);
                }
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

    public AnimalItem GetAnimalItem(GameObject gameObject)
    {
        if (animalDataDictionary.ContainsKey(gameObject))
        {
            return animalDataDictionary[gameObject];
        }
        return null;
    }
    private float CheckDirection(float width, float change)
    {
        if (width < 0)
        {
            return width - change;
        }
        return width + change;
    }
    private Vector3 GetRandomPosition()
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
        Vector3 spawnPosition = GetRandomPosition();
        GameObject animal = Instantiate(GetPrefab(itemName), spawnPosition, Quaternion.identity, transform);
        animalDataDictionary.Add(animal, GetAnimalData(itemName));
    }
}
