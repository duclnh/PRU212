using System.Linq;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    CapsuleCollider2D myBodyCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    public InventoryManager inventoryManager;

    public TileManager tileManager;


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
        inventoryManager = GetComponent<InventoryManager>();
    }
    // Update is called once per frame
    void Update()
    {
        Run();
        InteractiveGround();
        FlipSprite();
    }

    private void InteractiveGround()
    {
        if (tileManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3Int posistion = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y, 0);
                string tileName = tileManager.GetTileName(posistion);
                if (!string.IsNullOrEmpty(tileName))
                {
                    if (tileName == "Interactable" && inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
                    {
                         myAnimator.SetBool("IsDownWalking", true);
                        tileManager.SetInteracted(posistion);
                    }
                }
            }
        }
    }

    private void Run()
    {
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
