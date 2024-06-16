using Assets.Static;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    private MenuSettings menuSettings;
    private MapManager mapManager;
    [SerializeField] Button saveButton;
    void Start()
    {
        menuSettings = FindObjectOfType<MenuSettings>();
        mapManager = FindObjectOfType<MapManager>();
    }

    // Update is called once per frame
    public void Save()
    {
        StartCoroutine(SaveResources(menuSettings.userId, menuSettings.money));
    }

    private IEnumerator SaveResources(Guid userId, int money)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"userId\": \"{0}\", \"money\": \"{1}\"}}", userId, money);

        // Chuyển dữ liệu JSON thành byte array
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu PUT đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl + "User/" + userId, "PUT"))
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
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject userData = JObject.Parse(jsonResponse);
                string message = (string)userData["message"];
                GameManager.instance.nofification.Show(message);
                Debug.Log("Update money message: " + message);
            }
        }
    }
}
