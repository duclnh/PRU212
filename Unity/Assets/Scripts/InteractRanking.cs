using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRanking : MonoBehaviour
{
    private MapManager mapManager;
    [SerializeField] GameObject rankingMoney;
    [SerializeField] GameObject rankingIQ;

    // Start is called before the first frame update

    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
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
            ShowUpRanking();
            ShowUpRankingIQ();
        }
    }


    void ShowUpRanking()
    {
        rankingMoney.SetActive(true);
    }

    void ShowUpRankingIQ()
    {
        rankingIQ.SetActive(true);
    }

    public void CloseRanking()
    {
        rankingMoney.SetActive(false);
    }

    public void CloseRankingIQ()
    {
        rankingIQ.SetActive(false);
    }
}
