using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecTable : MonoBehaviour
{
    [SerializeField] AudioClip pickUpItem;
    public CollectionType type;
    public Sprite icon;
    public Rigidbody2D rigidbody2D;
    void Awake(){
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if(player){
            player.inventory.Add(this);
            AudioSource.PlayClipAtPoint(pickUpItem, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
public enum CollectionType {
    NONE, PADDY_SEED, MANGOSTEEN_SEED, TOMATO_SEED, POTATO_SEED
}
