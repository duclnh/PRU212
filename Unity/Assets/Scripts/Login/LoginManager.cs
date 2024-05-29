using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System;
using Assets.Static;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private MapManager mapManager;

    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;

    private static Guid _userId;
    [HideInInspector]
    public static Guid userId
    {
        get { return _userId; }
    }

    [SerializeField]
    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    /// <summary>
    ///     This function will be called when the user clicks on the login button
    /// </summary>
    public void OnSubmitLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string loginInfo = CheckLoginInfo(username, password);
        if (string.IsNullOrEmpty(loginInfo))
        {
            StartCoroutine(Login(username, password));
            //StartCoroutine(CreateUser("ducle", "123"));
        }
        else
        {
            Debug.LogError("Login failed: " + loginInfo);
        }
    }

    /// <summary>
    ///     Checks that the login information is acceptable and correct.
    /// </summary>
    /// <returns>This wil return an empty or null string for correct login. Otherwise it will return a string that explains the error.</returns>
    public string CheckLoginInfo(string username, string password)
    {
        string returnString = "";
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            returnString = "Username or password or both is empty";
        }
        return returnString;
    }

    /*private IEnumerator CreateUser(string username, string password)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"username\": \"{0}\", \"password\": \"{1}\"}}", username, password);
        Debug.Log(jsonData);

        // Chuyển dữ liệu JSON thành byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl, "POST"))
        {
            // Thiết lập tiêu đề Content-Type là application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Thiết lập UploadHandler để gửi dữ liệu JSON như là request body
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);

            // Thiết lập DownloadHandler để nhận phản hồi từ API
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
                Debug.Log("Status: " + webRequest.responseCode);
                Debug.Log("Add successful!");
            }
        }
    }*/

    private IEnumerator Login(string username, string password)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"username\": \"{0}\", \"password\": \"{1}\"}}", username, password);

        // Chuyển dữ liệu JSON thành byte array
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl + "login", "POST"))
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
                Guid userId = (Guid)userData["userId"];
                _userId = userId;
                mapManager.Play();
                Debug.Log("Login successful!");
            }
        }
    }


    /*private IEnumerator GetUser(string username, string password)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiClient.apiUrl + username))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Hihi: " + webRequest.result);
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject userData = JObject.Parse(jsonResponse);
                string usernameGet = (string)userData["username"];
                Guid userId = (Guid)userData["userId"];
                if (usernameGet.Equals(username))
                {
                    Debug.Log("usernameGet: " + usernameGet);
                    Debug.Log("userId: " + userId);
                    Debug.Log("Login success");
                }
            }
        }
    }*/
}
