using Assets.Static;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoadRanking : MonoBehaviour
{
    [SerializeField] GameObject rankingMoney;
    public List<TextMeshProUGUI> usernameList;
    public List<TextMeshProUGUI> moneyList;
    [SerializeField] TextMeshProUGUI textUserPosition;
    [SerializeField] TextMeshProUGUI textUserData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRankingByMoney());
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void LoadRanking()
    {
        StartCoroutine(GetRankingByMoney());
    }*/

    IEnumerator GetRankingByMoney()
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
                JArray list = (JArray)data["userRankings"];
                string currentRankMoney = (string)data["currentRankMoney"];
                string money = (string)data["money"];
                for (int i = 0; i < usernameList.Count; i++)
                {
                    if (i < list.Count)
                    {
                        usernameList[i].text = $"{(string)list[i]["rankMoney"]} . {(string)list[i]["username"]}";
                    }
                    else
                    {
                        usernameList[i].text = "";
                    }
                }
                for (int i = 0; i < moneyList.Count; i++)
                {
                    if (i < list.Count)
                    {
                        moneyList[i].text = (string)list[i]["money"];
                    }
                    else
                    {
                        moneyList[i].text = "";
                    }
                }
                if (textUserPosition != null && textUserData != null)
                {
                    textUserPosition.text = currentRankMoney;
                    textUserData.text = money;
                }
            }
        }
    }
}
