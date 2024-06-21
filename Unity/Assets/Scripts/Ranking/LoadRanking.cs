using Assets.Static;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class LoadRanking : MonoBehaviour
{
    [SerializeField] GameObject rankingMoney;
    public List<TextMeshProUGUI> usernameList;
    public List<TextMeshProUGUI> moneyList;
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
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiClient.apiUrl + $"User/ranking"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                JArray list = JArray.Parse(jsonResponse);
                for (int i = 0; i < list.Count; i++)
                {
                    usernameList[i].text = (string)list[i]["username"];
                }
                for (int i = 0; i < list.Count; i++)
                {
                    moneyList[i].text = (string)list[i]["money"];
                }
            }
        }
    }
}
