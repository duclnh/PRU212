using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CollecTable : MonoBehaviour
{
    [SerializeField] AudioClip pickUpItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player)
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                player.inventory.Add("Backpack",item);
                AudioSource.PlayClipAtPoint(pickUpItem, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        }
    }
}
