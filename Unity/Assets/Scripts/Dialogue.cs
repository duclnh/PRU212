using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    void Start()
    {
        ToggleDialogueBox();
    }

    public void ToggleDialogueBox()
    {
        if (dialogueBox != null)
        {
            if (!dialogueBox.activeSelf)
            {
                dialogueBox.SetActive(true);
                GameManager.instance.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0F, 0F);
            }
            else
            {
                dialogueBox.SetActive(false);
            }
        }
    }
    public void ShowStore()
    {
        ToggleDialogueBox();
        GameManager.instance.store.ToggleStore();
    }
    public bool ToggleStatus() => dialogueBox.activeSelf;
}
