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
        GameManager.instance.store.ToogleStore();
    }
    public bool ToggleStatus() => dialogueBox.activeSelf;
}
