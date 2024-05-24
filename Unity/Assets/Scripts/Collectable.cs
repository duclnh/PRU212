using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CollecTable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player)
        {
            Item item = GetComponent<Item>();
            if (item != null)
            {
                player.inventoryManager.Add("Backpack",item);
                GameManager.instance.menuSettings.SoundPickItem();
                Destroy(this.gameObject);
            }
        }
    }
}
