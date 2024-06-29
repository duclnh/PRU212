using Assets.Static;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoadRankingIQ : MonoBehaviour
{
    [SerializeField] GameObject rankingIQ;
    public List<TextMeshProUGUI> usernameList;
    public List<TextMeshProUGUI> countList;
    [SerializeField] TextMeshProUGUI textUserPosition;
    [SerializeField] TextMeshProUGUI textUserData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRankingByIQ());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetRankingByIQ()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiClient.apiUrl + $"User/ranking/{GameManager.instance.menuSettings.userId}"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JObject data = JObject.Parse(jsonResponse);
                JArray list = (JArray)data["countRightAnswer"];
                string currentRankIQ = (string)data["currentRankIQ"];
                string rightAnswer = (string)data["rightAnswer"];
                for (int i = 0; i < usernameList.Count; i++)
                {
                    if (i < list.Count)
                    {
                        usernameList[i].text = $"{i + 1}. {(string)list[i]["username"]}";
                    }
                    else
                    {
                        usernameList[i].text = "";
                    }
                }
                for (int i = 0; i < countList.Count; i++)
                {
                    if (i < list.Count)
                    {
                        countList[i].text = (string)list[i]["count"];
                    }
                    else
                    {
                        countList[i].text = "";
                    }
                }
                if (textUserPosition != null && textUserData != null)
                {
                    textUserPosition.text = currentRankIQ;
                    textUserData.text = rightAnswer;
                }
            }
        }
    }
}
