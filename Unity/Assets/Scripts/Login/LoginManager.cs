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
    private MenuSettings menuSettings;
    private NotificationManager notificationManager;

    [SerializeField] GameObject formRegister;

    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;
    private void Awake()
    {
        menuSettings = FindObjectOfType<MenuSettings>();
        notificationManager = FindObjectOfType<NotificationManager>();
    }
    public void TogggleRegister()
    {
        if (formRegister != null)
        {
            notificationManager.OnCloseMessage();
            formRegister.SetActive(true);
        }
        gameObject.SetActive(false);
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
        }
        else
        {
            notificationManager.OnShowMessage("Login failed: " + loginInfo);
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

    private IEnumerator Login(string username, string password)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"username\": \"{0}\", \"password\": \"{1}\"}}", username, password);

        // Chuyển dữ liệu JSON thành byte array
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl + "User/login", "POST"))
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
                notificationManager.OnShowMessage("Login failed: " + message);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject userData = JObject.Parse(jsonResponse);
                Guid userId = (Guid)userData["data"]["userId"];
                int money = (int)userData["data"]["money"];
                float x = (float)userData["data"]["positionX"];
                float y = (float)userData["data"]["positionY"];
                float z = (float)userData["data"]["positionZ"];
                string scene = (string)userData["data"]["sence"];
                menuSettings.userId = userId;
                menuSettings.money = money;
                if (!string.IsNullOrEmpty(scene))
                {
                    menuSettings.playVector = new Vector3(x, y, z);
                    menuSettings.sceneName = scene;
                    SceneManager.LoadScene(scene);
                }
                else
                {
                    SceneManager.LoadScene("Sence 1");
                }

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
