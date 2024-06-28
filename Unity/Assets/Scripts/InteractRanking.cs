using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractRanking : MonoBehaviour
{
    [SerializeField] GameObject rankingMoney;
    [SerializeField] GameObject rankingIQ;
    [SerializeField] GameObject TabRanking;


    /*private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            NextScene();
        }
    }

    void NextScene()
    {
        mapManager.Ranking();
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            /*ShowUpRanking();
            ShowUpRankingIQ();*/
            ShowUpRanking();
        }
    }


    /*public void ShowUpRankingMoney()
    {
        rankingMoney.SetActive(true);
    }

    public void ShowUpRankingIQ()
    {
        rankingIQ.SetActive(true);
    }

    public void CloseRankingMoney()
    {
        rankingMoney.SetActive(false);
    }

    public void CloseRankingIQ()
    {
        rankingIQ.SetActive(false);
    }*/

    void ShowUpRanking()
    {
        TabRanking.SetActive(true);
    }

    public void CloseRanking()
    {
        TabRanking.SetActive(false);
    }
}
