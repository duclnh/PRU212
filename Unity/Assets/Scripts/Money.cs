using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        playerMovement = GameManager.instance.player;
        RenderMoney();
    }

    public void RenderMoney()
    {
        textMeshProUGUI.text = "" + GameManager.instance.menuSettings.money;
        Debug.Log("Money: " + playerMovement.money);    
    }
}
