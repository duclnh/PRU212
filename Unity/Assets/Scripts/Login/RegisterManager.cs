using Assets.Static;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterManager : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField password1InputField;

    [SerializeField]
    private TMP_InputField password2InputField;

    [SerializeField] 
    private GameObject formLogin;

    private NotificationManager notificationManager;
    void Start()
    {
        gameObject.SetActive(false);
        notificationManager = FindObjectOfType<NotificationManager>();
    }
    public void Back()
    {
        if (formLogin != null)
        {
            formLogin.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public void OnSubmitRegister()
    {
        string username = usernameInputField.text;
        string password1 = password1InputField.text;
        string password2 = password2InputField.text;

        if (username.Length > 20)
        {
            notificationManager.OnShowMessage("Username more than 20 characters");
            return;
        }
        if (password1.Length > 20)
        {
            notificationManager.OnShowMessage("Password more than 20 characters");
            return;
        }

        string registerInfo = CheckRegisterInfo(username, password1, password2);
        if (string.IsNullOrEmpty(registerInfo))
        {
            StartCoroutine(CreateUser(username, password1));
        }
        else
        {
            usernameInputField.text = string.Empty;
            password1InputField.text = string.Empty;
            password2InputField.text = string.Empty;
            notificationManager.OnShowMessage("Register failed: " + registerInfo);
        }
    }

    public string CheckRegisterInfo(string username, string password1, string password2)
    {
        string returnString = "";
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2))
        {
            returnString = "Username or password or both is empty";
        }
        if (password1 != password2)
        {
            returnString = "Password and confirm password do not match";
        }
        return returnString;
    }


    private IEnumerator CreateUser(string username, string password)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"username\": \"{0}\", \"password\": \"{1}\"}}", username, password);
        Debug.Log(jsonData);

        // Chuyển dữ liệu JSON thành byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl + "User/", "POST"))
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
                string jsonResponse = webRequest.downloadHandler.text;
                JObject userData = JObject.Parse(jsonResponse);
                string message = (string)userData["message"];
                notificationManager.OnShowMessage("Login failed: " + message);
            }
            else
            {
                Debug.Log("Add successful!");
            }
        }
    }
}
