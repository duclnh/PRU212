using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRanking : MonoBehaviour
{
    private MapManager mapManager;
    private bool status = true;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        /*if (status)
        {
            NextScene();
        }
        else
        {
            GameManager.instance.nofification.Show($"Please come back here later.");
        }*/
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if(player != null)
        {
            NextScene();
        }
    }

    void NextScene()
    {
        mapManager.Ranking();
    }
}
