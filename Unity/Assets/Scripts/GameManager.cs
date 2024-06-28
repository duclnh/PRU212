using System.Collections;
using System.Collections.Generic;
using Assets.Static;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

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
    public void SaveData()
    {
        List<ItemSlot> itemSlots = new List<ItemSlot>();
        Inventory backPack = player.inventoryManager.GetInventoryByName("Backpack");
        for (int i = 0; i < backPack.slots.Count; i++)
        {
            itemSlots.Add(new ItemSlot
            {
                SlotId = i,
                ItemName = backPack.slots[i].itemName,
                Icon = AssetDatabase.GetAssetPath(backPack.slots[i].icon),
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
                Icon = AssetDatabase.GetAssetPath(toolBar.slots[i].icon),
                Amount = toolBar.slots[i].count,
                Type = "Toolbar",
                UserId = player.idUser
            });
        }

        List<PlantTable> plantTables = new List<PlantTable>();
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

            Debug.Log($"PlantId: 1 ,x:{slot.x} ,y:{slot.y} z:, {slot.z}, {cropDataDictionary[slot].ToString()},UserID: {GameManager.instance.player.idUser}");
        }
        List<AnimalTable> animalTables = new List<AnimalTable>();
        Dictionary<GameObject, AnimalItem> animalDataDictionary = animalManager.animalDataDictionary;
        foreach (var animal in animalDataDictionary.Keys)
        {
            animalTables.Add(new AnimalTable
            {
                PositionX = animal.transform.lossyScale.x,
                PositionY = animal.transform.lossyScale.y,
                PositionZ = animal.transform.lossyScale.z,
                // MoveSpeed = animalDataDictionary[animal].animalData.moveSpeed,
                // NameItem = animalDataDictionary[animal].animalData.nameItem,
                // GrowTime = animalDataDictionary[animal].animalData.growTime,
                // NumberStage = animalDataDictionary[animal].animalData.numberStage,
                // Price = animalDataDictionary[animal].animalData.price,
                // Quantity = animalDataDictionary[animal].animalData.quantity,
                ItemName = animalDataDictionary[animal].itemName,
                Datetime = animalDataDictionary[animal].dateTime,
                CurrentStage = animalDataDictionary[animal].currentStage,
                QuantityHarvested = animalDataDictionary[animal].quantityHarvested,
                PriceHarvested = animalDataDictionary[animal].priceHarvested,
                Hungry = animalDataDictionary[animal].hungry,
                Sick = animalDataDictionary[animal].sick,
                UserId = player.idUser
            });
            Debug.Log($"AnimalId: 1 ,x:{animal.transform.lossyScale.x} ,y:{animal.transform.lossyScale.y} z:, {animal.transform.lossyScale.z}, {animalDataDictionary[animal].ToString()},UserID: {GameManager.instance.player.idUser}");
        }
    }

    public void GetData()
    {
        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Sprout Lands - Sprites - Basic pack/Objects/Basic_Plants_5.png");
        Item item = new Item();
        item.data = new ItemData
        {
            itemName = "Paddy Sells",
            icon = icon,
            price = 40,
        };
        item.amount = 300;
        player.inventoryManager.Add("Backpack", item);
    }
    private IEnumerator SaveDataGame(string username, string password)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"userInfor\": \"{0}\", \"animals\": \"{1}\", \"plants\": \"{1}\"}}", username, password);

        // Chuyển dữ liệu JSON thành byte array
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest($"{ApiClient.apiUrl}User/{player.idUser}" + "User/login", "PUT"))
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
                //Debug.LogError("Error: " + webRequest.error);
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log(jsonResponse);
                JObject userData = JObject.Parse(jsonResponse);
                string message = (string)userData["message"];
                nofification.Show("SaveData failed: " + message);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                nofification.Show("Save successfully");
            }
        }
    }
}

public class AnimalTable
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    // public float MoveSpeed { get; set; }
    // public string NameItem { get; set; }
    // public float GrowTime { get; set; }
    // public int NumberStage { get; set; }
    // public int Price { get; set; }
    // public int Quantity { get; set; }
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
    // public string Crop { get; set; }
    // public int GrowTime { get; set; }
    // public int Quantity { get; set; }
    // public string Tiles { get; set; }
    public System.Guid UserId { get; set; }
}
public class ItemSlot
{
    public int SlotId { get; set; }
    public string ItemName { get; set; }
    public string Icon { get; set; }
    // public int Price { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; }
    public System.Guid UserId { get; set; }
}

public class UserInfor
{
    public string UserId { get; set; }
    public int Money { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    public string Sence { get; set; }
}

