using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float speed = 0.2f;
    Rigidbody2D myRigidbody;
    bool stayPlayer = false;
    Animator animalAnimation;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animalAnimation = GetComponent<Animator>();
    }


    void Update()
    {
        if (stayPlayer)
        {

            myRigidbody.velocity = new Vector2(0F, 0F);
        }
        else
        {
            myRigidbody.velocity = new Vector2(speed, 0F);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player" && GameManager.instance.player.TakeMilk())
        {

            Debug.Log(gameObject.name);
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
