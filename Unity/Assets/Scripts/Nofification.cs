using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nofification : MonoBehaviour
{
    [SerializeField] GameObject notificationPannel;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        if (notificationPannel != null)
        {
            notificationPannel.SetActive(false);
        }
    }

    public void Show(string text){
        notificationPannel.SetActive(true);
        StartCoroutine(MessageBox(text));
    }
    private IEnumerator MessageBox(string text)
    {
        textMeshProUGUI.text = text;
        yield return new WaitForSeconds(1.2f);
        notificationPannel.SetActive(false);
    }

}
