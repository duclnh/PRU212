using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float speed = 0.2f;
    Rigidbody2D myRigidbody;
    bool stayPlayer = false;
    Animator animalAnimation;
    [SerializeField] Item item;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animalAnimation = GetComponent<Animator>();
    }


    void Update()
    {
        if (!GameManager.instance.menuSettings.GetStatusMenuSetting())
        {
            if (stayPlayer)
            {

                myRigidbody.velocity = new Vector2(0F, 0F);
            }
            else
            {
                myRigidbody.velocity = new Vector2(speed, 0F);
            }
            InteractWithAnimal();
        }

    }
    private void InteractWithAnimal()
    {
        if (Input.GetKeyDown(KeyCode.Q) && stayPlayer)
        {
            if (gameObject.name.Contains("Cow"))
            {
                if (GameManager.instance.player.inventoryManager.toolbar.selectedSlot.itemName == "Bottle")
                {
                    AnimalItem animalItem = GameManager.instance.animalManager.GetAnimalItem(gameObject);
                    if (animalItem.currentStage >= animalItem.animalData.numberStage - 2)
                    {
                        if (animalItem.animalData.quantity > 0)
                        {
                            if (GameManager.instance.player.inventoryManager.toolbar.selectedSlot.count > 0)
                            {
                                GameManager.instance.player.inventoryManager.Remove("Toolbar", "Bottle");
                                GameManager.instance.player.DropItem(item, 1);
                                animalItem.quantityHarvested -= 1;
                                GameManager.instance.uiManager.RefreshInventoryUI("Toolbar");
                            }

                        }
                        else
                        {
                            GameManager.instance.nofification.Show("The cow has no milk");
                            GameManager.instance.menuSettings.SoundFail();
                        }
                    }
                    else
                    {
                        GameManager.instance.nofification.Show("The cow has no milk yet");
                        GameManager.instance.menuSettings.SoundFail();
                    }


                }
                else
                {
                    GameManager.instance.nofification.Show("Your hand is not holding the bottle yet");
                    GameManager.instance.menuSettings.SoundFail();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "Player")
        {
            speed = -speed;
            FlipSprites();
        }
        else
        {
            animalAnimation.enabled = false;
            stayPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            animalAnimation.enabled = true;
            stayPlayer = false;
        }
    }

    private void FlipSprites()
    {
        transform.localScale = new Vector3(-transform.localScale.x,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }
}
