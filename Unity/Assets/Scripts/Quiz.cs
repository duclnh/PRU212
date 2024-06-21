using Assets.Static;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] GameObject quizTable;
    [SerializeField] TextMeshProUGUI textMeshQuestion;
    [SerializeField] TextMeshProUGUI textMeshA;
    [SerializeField] TextMeshProUGUI textMeshB;
    [SerializeField] TextMeshProUGUI textMeshC;
    [SerializeField] TextMeshProUGUI textMeshD;
    [SerializeField] Button answer1Button;
    [SerializeField] Button answer2Button;
    [SerializeField] Button answer3Button;
    [SerializeField] Button answer4Button;
    [SerializeField] int timeToAnswer = 15;
    private Guid questionId;
    private string result = "A";
    private DateTime FailAnswer = DateTime.Now;
    private bool status = true;

    private bool isGetQuestion = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (status)
        {
            ToggleQuiz();
        }
        else
        {
            GameManager.instance.nofification.Show($"Please come back later here ({FailAnswer.ToString("HH:mm")})");
        }
    }
    void Update()
    {
        if (DateTime.UtcNow == FailAnswer)
        {
            status = true;
        }
    }
    public void ToggleQuiz()
    {
        if (quizTable != null)
        {
            if (!quizTable.activeSelf)
            {
                GameManager.instance.player.SetMove(false);
                quizTable.SetActive(true);
                StartCoroutine(GetQuestion());
                if (!isGetQuestion)
                {
                    return;
                }
                quizTable.SetActive(true);
                answer1Button.onClick.AddListener(Answer1);
                answer2Button.onClick.AddListener(Answer2);
                answer3Button.onClick.AddListener(Answer3);
                answer4Button.onClick.AddListener(Answer4);
                StartCoroutine(AnswerQuestionDelay());
            }
            else
            {
                quizTable.SetActive(false);
            }
        }
    }

    private IEnumerator AnswerQuestionDelay()
    {
        yield return new WaitForSeconds(timeToAnswer);
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
    }

    private IEnumerator GetQuestion()
    {
        yield return StartCoroutine(GetQuestionn());
    }

    private IEnumerator GetQuestionn()
    {
        var userId = GameManager.instance.menuSettings.userId;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiClient.apiUrl + $"Question/{userId}"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject question = JObject.Parse(jsonResponse);
                questionId = (Guid)question["questionId"];
                if(questionId == Guid.Empty)
                {
                    isGetQuestion = false;
                    yield return null;
                }    
                textMeshQuestion.text = (string)question["question1"];
                textMeshA.text = (string)question["optionA"]; ;
                textMeshB.text = (string)question["optionB"]; ;
                textMeshC.text = (string)question["optionC"]; ;
                textMeshD.text = (string)question["optionD"]; ;
                result = (string)question["answer"];
            }
        }
    }
    public void Answer1()
    {
        if ("A" == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
            OnSave(result);
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer2()
    {
        if ("B" == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
            OnSave(result);
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer3()
    {
        if ("C" == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
            OnSave(result);
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer4()
    {
        if ("D" == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
            OnSave(result);
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }

    private void OnSave(string result)
    {
        StartCoroutine(SaveRecord(GameManager.instance.menuSettings.userId, questionId));
    }

    private IEnumerator SaveRecord(Guid userId, Guid questionId)
    {
        // Tạo dữ liệu JSON từ thông tin người dùng
        string jsonData = string.Format("{{\"userId\": \"{0}\", \"questionId\": \"{1}\"}}", userId, questionId);
        Debug.Log(jsonData);

        // Chuyển dữ liệu JSON thành byte array
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Gửi yêu cầu POST đến API với request body là dữ liệu JSON
        using (UnityWebRequest webRequest = new UnityWebRequest(ApiClient.apiUrl + "Record", "POST"))
        {
            // Thiết lập tiêu đề Content-Type là application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Thiết lập UploadHandler để gửi dữ liệu JSON như là request body
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);

            // Thiết lập DownloadHandler để nhận phản hồi từ API
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // Gửi yêu cầu và chờ phản hồi
            yield return webRequest.SendWebRequest();
        }
    }
}
