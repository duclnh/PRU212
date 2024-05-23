using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] public int money = 500;
    Vector2 moveInput;
    CapsuleCollider2D myBodyCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    public InventoryManager inventoryManager;

    public TileManager tileManager;
    public CropManger cropManger;

    public bool BuyItemStore(int price)
    {
        if (money - price >= 0)
        {
            money -= price;
            GameManager.instance.money.RenderMoney();
            return true;
        }
        return false;
    }
    public void SellItemStore(int price)
    {
        money += price;
        GameManager.instance.money.RenderMoney();

    }
    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffSet = Random.insideUnitCircle * 1.25f;
        Item dropItem = Instantiate(item, spawnLocation + spawnOffSet, Quaternion.identity);
        dropItem.rigidbody2D.AddForce(spawnOffSet * 2f, ForceMode2D.Impulse);
    }
    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        tileManager = GameManager.instance.tileManager;
        cropManger = GameManager.instance.cropManger;
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        InteractiveGround();
        InteractivePlowing();
        Harvest();
        FlipSprite();
    }

    private void Harvest()
    {
        if (tileManager != null && cropManger != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3Int position = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y, 0);
                string cropName = cropManger.GetTileName(position);
                string tileName = tileManager.GetTileName(position);
                if (cropName != "Interactable")
                {
                    if (cropManger.HarvestPlant(position))
                    {
                        tileManager.RestoreIntered(position);
                    }
                }
                else
                {
                    GameManager.instance.nofification.Show("Please dig the soil before planting");
                }
            }
        }
    }

    private void InteractivePlowing()
    {
        if (tileManager != null && cropManger != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3Int position = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, 0);
                string cropName = cropManger.GetTileName(position);
                string tileName = tileManager.GetTileName(position);
                if (tileName == "Summer_Plowed" && cropName == "Interactable")
                {
                    if (inventoryManager.toolbar.selectedSlot.itemName.Contains("Seed"))
                    {
                        cropManger.Seed(position, inventoryManager.toolbar.selectedSlot.itemName);
                    }
                    else
                    {
                        GameManager.instance.nofification.Show("Choose any seed to plant");
                    }
                }
                else
                {
                    GameManager.instance.nofification.Show("Please dig the soil before planting");
                }
            }
        }
    }

    private IEnumerator InteractiveGroundWithDelay()
    {

        Vector3Int position = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, 0);
        string tileName = tileManager.GetTileName(position);
        if (!string.IsNullOrEmpty(tileName))
        {
            if (inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
            {
                if (tileName == "Interactable")
                {
                    // Perform animation and interaction
                    myAnimator.SetBool("IsPlowing", true);

                    yield return new WaitForSeconds(0.5f); // Adjust delay time if needed
                    tileManager.SetInteracted(position);
                }
            }
            else
            {
                GameManager.instance.nofification.Show("You need select hoe");
            }
        }

        // Stop animation
        myAnimator.SetBool("IsPlowing", false);
    }

    private void InteractiveGround()
    {
        if (tileManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(InteractiveGroundWithDelay());
            }
        }
    }

    private void Run()
    {
        if (GameManager.instance.dialogue.ToggleStatus()
            || myAnimator.GetBool("IsPlowing")
            || GameManager.instance.store.ToggleStoreStatus())
        {
            myAnimator.SetBool("IsDownWalking", false);
            myAnimator.SetBool("IsUpWalking", false);
            myAnimator.SetBool("IsTurnWalking", false);
            return;
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, moveInput.y * runSpeed);
        myRigidbody.velocity = playerVelocity;

        bool isTurnWalking = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool isUpWalking = playerVelocity.y > 0;
        bool isDownWalking = playerVelocity.y < 0;

        myAnimator.SetBool("IsDownWalking", isDownWalking);
        myAnimator.SetBool("IsUpWalking", isUpWalking);
        myAnimator.SetBool("IsTurnWalking", isTurnWalking);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    private void FlipSprite()
    {
        bool playerTurnRight = myRigidbody.velocity.x > 0;
        bool playerTurnLeft = myRigidbody.velocity.x < 0;

        if (playerTurnRight)
        {
            transform.localScale = new Vector2(Mathf.Sign(-myRigidbody.velocity.x), 1f);
        }
        if (playerTurnLeft)
        {
            transform.localScale = new Vector2(Mathf.Sign(-myRigidbody.velocity.x), 1f);
        }
    }
}
