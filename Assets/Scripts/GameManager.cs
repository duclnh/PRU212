using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public UI_Manager uiManager;

    public Nofification nofification;

    public PlayerMovement player;
    public Dialogue dialogue;
    public Store  store;

    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();
        nofification = FindObjectOfType<Nofification>();
        dialogue = FindObjectOfType<Dialogue>();
        store = FindObjectOfType<Store>();
        player = FindObjectOfType<PlayerMovement>();
    }
}
