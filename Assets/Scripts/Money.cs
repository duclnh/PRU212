using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
   [SerializeField] PlayerMovement playerMovement;
   [SerializeField] TextMeshProUGUI textMeshProUGUI;

   private void Start(){
    playerMovement = GameManager.instance.player;
   }

    private void RenderMoney()
    {
        textMeshProUGUI.text = ""+ playerMovement.money;
    }
}
