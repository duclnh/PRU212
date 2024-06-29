using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Static;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public CropManger cropManger;
    public UI_Manager uiManager;
    public AnimalManager animalManager;
    public Nofification nofification;

    public PlayerMovement player;
    public Dialogue dialogue;
    public Store store;

    public Money money { get; set; }

    public MenuSettings menuSettings;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        cropManger = GetComponent<CropManger>();
        uiManager = GetComponent<UI_Manager>();
        animalManager = GetComponent<AnimalManager>();
        menuSettings = FindObjectOfType<MenuSettings>();
        nofification = FindObjectOfType<Nofification>();
        dialogue = FindObjectOfType<Dialogue>();
        store = FindObjectOfType<Store>();
        player = FindObjectOfType<PlayerMovement>();
        money = FindObjectOfType<Money>();

    }
    void Start()
    {
        StartCoroutine(LoadDataGame());
    }

    public void SaveDataAndPerformActions(string scene)
    {
        StartCoroutine(SaveData(scene));
    }
    public IEnumerator SaveData(string scene)
    {
        UserInfor user = new UserInfor
        {
            UserId = player.idUser,
            Money = player.money,
            PositionX = player.transform.position.x,
            PositionY = player.transform.position.y,
            PositionZ = player.transform.position.z,
            Sence = SceneManager.GetActiveScene().name,
        };
        List<ItemSlot> itemSlots = new List<ItemSlot>();
        Inventory backPack = player.inventoryManager.GetInventoryByName("Backpack");
        for (int i = 0; i < backPack.slots.Count; i++)
        {
            itemSlots.Add(new ItemSlot
            {
                SlotId = i,
                ItemName = backPack.slots[i].itemName,
                Amount = backPack.slots[i].count,
                Type = "Backpack",
                UserId = player.idUser
            });
        }
        Inventory toolBar = player.inventoryManager.GetInventoryByName("Toolbar");
        for (int i = 0; i < toolBar.slots.Count; i++)
        {
            itemSlots.Add(new ItemSlot
            {
                SlotId = i,
                ItemName = toolBar.slots[i].itemName,
                Amount = toolBar.slots[i].count,
                Type = "Toolbar",
                UserId = player.idUser
            });
        }

        List<AnimalTable> animalTables = new List<AnimalTable>();
        List<PlantTable> plantTables = new List<PlantTable>();
        if (SceneManager.GetActiveScene().name == "Sence 1")
        {
            Dictionary<Vector3Int, CropItem> cropDataDictionary = cropManger.cropDataDictionary;
            foreach (var slot in cropDataDictionary.Keys)
            {
                plantTables.Add(new PlantTable
                {
                    PositionX = slot.x,
                    PositionY = slot.y,
                    PositionZ = slot.z,
                    Crop = cropDataDictionary[slot].itemName,
                    Datetime = cropDataDictionary[slot].dateTime,
                    CurrentStage = cropDataDictionary[slot].currentStage,
                    QuantityHarvested = cropDataDictionary[slot].quantityHarvested,
                    UserId = player.idUser
                });
            }
            Dictionary<GameObject, AnimalItem> animalDataDictionary = animalManager.animalDataDictionary;
            foreach (var animal in animalDataDictionary.Keys)
            {
                animalTables.Add(new AnimalTable
                {
                    PositionX = animal.transform.position.x,
                    PositionY = animal.transform.position.y,
                    PositionZ = animal.transform.position.z,
                    localScaleX = animal.transform.localScale.x < 0 ?  animal.transform.localScale.x * -1 :  animal.transform.localScale.x,
                    localScaleY = animal.transform.localScale.y,
                    ItemName = animalDataDictionary[animal].itemName,
                    Datetime = animalDataDictionary[animal].dateTime,
                    CurrentStage = animalDataDictionary[animal].currentStage,
                    QuantityHarvested = animalDataDictionary[animal].quantityHarvested,
                    PriceHarvested = animalDataDictionary[animal].priceHarvested,
                    Hungry = animalDataDictionary[animal].hungry,
                    Sick = animalDataDictionary[animal].sick,
                    UserId = player.idUser
                });
            }
        }
        yield return StartCoroutine(SaveDataGame(plantTables, animalTables, itemSlots, user, scene));
        if (!string.IsNullOrEmpty(scene))
        {
            SceneManager.LoadScene(scene);
        }
    }

    private IEnumerator SaveDataGame(List<PlantTable> plants, List<AnimalTable> animals, List<ItemSlot> itemSlots, UserInfor userInfor, string nextScene)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        var jsonData = new
        {
            userInfo = userInfor,
            itemsBackpack = itemSlots.Where(x => x.Type == "Backpack").ToList(),
            itemsToolbar = itemSlots.Where(x => x.Type == "Toolbar").ToList(),
            animals = animals,
            plants = plants
        };
        // Chuyển dữ liệu JSON thành byte array
        byte[] body = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonData));

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest($"{ApiClient.apiUrl}User/{player.idUser}", "PUT"))
        {
            // Thiết lập tiêu đề Content-Type là application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Thiết lập UploadHandler để gửi dữ liệu JSON như là request body
            webRequest.uploadHandler = new UploadHandlerRaw(body);

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // Gửi yêu cầu và chờ phản hồi
            yield return webRequest.SendWebRequest();

            // Kiểm tra lỗi và xử lý phản hồi
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                nofification.Show("SaveData failed");
            }
            else
            {
                nofification.Show("Save successfully");
            }
        }
    }
    private IEnumerator LoadDataGame()
    {
        using (UnityWebRequest webRequest = new UnityWebRequest($"{ApiClient.apiUrl}Item/{menuSettings.userId}", "GET"))
        {
            // Thiết lập tiêu đề Content-Type là application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");


            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // Gửi yêu cầu và chờ phản hồi
            yield return webRequest.SendWebRequest();

            // Kiểm tra lỗi và xử lý phản hồi
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {

            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject data = JObject.Parse(jsonResponse);
                List<ItemSlot> backpack = data["itemsBackpack"].ToObject<List<ItemSlot>>();
                Sprite icon = null;
                Item item = new Item();
                foreach (ItemSlot itemBackpack in backpack)
                {
                    icon = store.GeticonFromStore(itemBackpack.ItemName);
                    item.data = new ItemData
                    {
                        itemName = itemBackpack.ItemName,
                        icon = icon,
                    };
                    item.amount = itemBackpack.Amount;
                    player.inventoryManager.Add("Backpack", item, itemBackpack.SlotId);
                }
                List<ItemSlot> toolbar = data["itemsToolbar"].ToObject<List<ItemSlot>>();
                foreach (ItemSlot itemToolbar in toolbar)
                {
                    icon = store.GeticonFromStore(itemToolbar.ItemName);
                    item.data = new ItemData
                    {
                        itemName = itemToolbar.ItemName,
                        icon = icon,
                    };
                    item.amount = itemToolbar.Amount;
                    player.inventoryManager.Add("Toolbar", item, itemToolbar.SlotId);
                }
                uiManager.RefreshAll();
                if (SceneManager.GetActiveScene().name == "Sence 1")
                {
                    List<AnimalTable> animal = data["animals"].ToObject<List<AnimalTable>>();
                    foreach (AnimalTable animalTable in animal)
                    {
                        animalManager.DropAnimal(animalTable);
                    }
                    List<PlantTable> plant = data["plants"].ToObject<List<PlantTable>>();
                    foreach (PlantTable plantTable in plant)
                    {
                        cropManger.Seed(plantTable);
                        tileManager.SetInteracted(new Vector3Int(plantTable.PositionX, plantTable.PositionY, plantTable.PositionZ));
                    }
                }
            }
        }
    }
}

public class AnimalTable
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public float localScaleX { get; set; }
    public float localScaleY { get; set; }
    public string ItemName { get; set; }
    public System.DateTime Datetime { get; set; }
    public int CurrentStage { get; set; }
    public int QuantityHarvested { get; set; }
    public int PriceHarvested { get; set; }
    public bool Hungry { get; set; }
    public bool Sick { get; set; }
    public System.Guid UserId { get; set; }
}

public class PlantTable
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int PositionZ { get; set; }
    public string Crop { get; set; }
    public System.DateTime Datetime { get; set; }
    public int CurrentStage { get; set; }
    public int QuantityHarvested { get; set; }
    public System.Guid UserId { get; set; }
}
public class ItemSlot
{
    public int SlotId { get; set; }
    public string ItemName { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; }
    public System.Guid UserId { get; set; }
}

public class UserInfor
{
    public System.Guid UserId { get; set; }
    public int Money { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public string Sence { get; set; }
}

