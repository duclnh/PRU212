using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNPC : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D collision2D)
     {
       GameManager.instance.dialogue.ToggleDialogueBox();
    }
}
