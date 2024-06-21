using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] Tabs;
    public Image[] TabButtons;
    public Sprite InactiveTab, ActiveTab;
    public Vector2 InactiveTabButtonSize, ActiveTabButtonSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTab(int TabId)
    {
        foreach (GameObject go in Tabs)
        {
            go.SetActive(false);
        }
        Tabs[TabId].SetActive(true);

        foreach (Image image in TabButtons)
        {
            image.sprite = InactiveTab;
            image.rectTransform.sizeDelta = InactiveTabButtonSize;
        }
        TabButtons[TabId].sprite = ActiveTab;
        TabButtons[TabId].rectTransform.sizeDelta = ActiveTabButtonSize;
    }
}
