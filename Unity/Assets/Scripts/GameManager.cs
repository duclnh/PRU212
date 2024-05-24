using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public CropManger cropManger;
    public UI_Manager uiManager;

    public Nofification nofification;

    public PlayerMovement player;
    public Dialogue dialogue;
    public Store  store;

    public Money money;

    public MenuSettings menuSettings;

    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        cropManger = GetComponent<CropManger>();
        uiManager = GetComponent<UI_Manager>();
        menuSettings = FindObjectOfType<MenuSettings>();
        nofification = FindObjectOfType<Nofification>();
        dialogue = FindObjectOfType<Dialogue>();
        store = FindObjectOfType<Store>();
        player = FindObjectOfType<PlayerMovement>();
        money = FindObjectOfType<Money>();  
    }
}
