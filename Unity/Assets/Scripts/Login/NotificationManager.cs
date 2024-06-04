using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{

    [SerializeField]
    private Canvas notification;

    [SerializeField]
    private TextMeshProUGUI message;

    [SerializeField]
    private Button closeButton;
    void Start()
    {
        notification = GetComponent<Canvas>();
        notification.enabled = false;
    }

    public void OnShowMessage(string message)
    {
        notification.enabled = true;
        this.message.text = message;
    }

    public void OnCloseMessage()
    {
        notification.enabled = false;
    }

    public void OnOpenMessage()
    {
        notification.enabled = true;
    }
}
