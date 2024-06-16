using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] GameObject interactTablePlant;
    [SerializeField] GameObject interactAnimal;
    public int money { get; set;}
    Vector2 moveInput;
    CapsuleCollider2D myBodyCollider;
    private Rigidbody2D myRigidbody;
    Animator myAnimator;
    private MenuSettings menuSettings;

    public InventoryManager inventoryManager;

    public TileManager tileManager;
    public CropManger cropManger;
    public System.Guid idUser { get; set; }
    private bool move = true;
    public void ToggleInteractPlant(bool interact)
    {
        if (interactTablePlant != null)
        {
            interactTablePlant.SetActive(interact);
        }
    }
    public void ToggleInteractAnimal(bool interact)
    {
        if (interactAnimal != null)
        {
            interactAnimal.SetActive(interact);
        }
    }
    public bool BuyItemStore(int price)
    {
        if (money - price >= 0)
        {
            money -= price;
            GameManager.instance.nofification.Show("-" + price);
            GameManager.instance.menuSettings.SoundBuyItem();
            menuSettings.money = money;
            GameManager.instance.money.RenderMoney();
            return true;
        }
        return false;
    }
    public void SellItemStore(int price)
    {
        money += price;
        GameManager.instance.nofification.Show("+" + price);
        GameManager.instance.menuSettings.SoundSellItem();
        GameManager.instance.money.RenderMoney();

    }
    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffSet = Random.insideUnitCircle * 1.25f;
        Item dropItem = Instantiate(item, spawnLocation + spawnOffSet, Quaternion.identity);
        dropItem.GetComponent<Rigidbody2D>().AddForce(spawnOffSet * 2f, ForceMode2D.Impulse);
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
        menuSettings = GameManager.instance.menuSettings;
        money = menuSettings.money;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.menuSettings.GetStatusMenuSetting())
        {
            if (move)
            {
                Run();
            }
            InteractiveGround();
            InteractivePlowing();
            Harvest();
            FlipSprite();
            MenuSetting();
            AreaPlating();
        }
    }

    private void AreaPlating()
    {
        Vector3Int position = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, 0);
        string titleName = tileManager.GetTileName(position);
        if (titleName != null && titleName == "Interactable")
        {
            ToggleInteractPlant(true);
        }
        else
        {
            ToggleInteractPlant(false);
        }
    }

    private void MenuSetting()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuSettings();
        }
    }

    public void ToggleMenuSettings()
    {
        GameManager.instance.menuSettings.ToggleMenu();
    }

    private void Harvest()
    {
        if (tileManager != null && cropManger != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3Int position = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, 0);
                string cropName = cropManger.GetTileName(position);
                if (!string.IsNullOrEmpty(cropName) && cropName != "Interactable")
                {
                    if (cropName != "Interactable")
                    {
                        if (cropManger.HarvestPlant(position))
                        {
                            tileManager.RestoreIntered(position);
                        }
                    }
                    else
                    {
                        GameManager.instance.menuSettings.SoundFail();
                        GameManager.instance.nofification.Show("Please dig the soil before planting");
                    }
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
                if (!string.IsNullOrEmpty(cropName) && !string.IsNullOrEmpty(tileName))
                {
                    if (tileName == "Summer_Plowed" && cropName == "Interactable")
                    {
                        if (inventoryManager.toolbar.selectedSlot.itemName.Contains("Seed"))
                        {
                            cropManger.Seed(position, inventoryManager.toolbar.selectedSlot.itemName);
                        }
                        else
                        {
                            GameManager.instance.menuSettings.SoundFail();
                            GameManager.instance.nofification.Show("Choose any seed to plant");
                        }
                    }
                    else
                    {
                        GameManager.instance.menuSettings.SoundFail();
                        GameManager.instance.nofification.Show("Please dig the soil before planting");
                    }
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

                    myAnimator.SetBool("IsPlowing", true);

                    yield return new WaitForSeconds(1.5f);
                    tileManager.SetInteracted(position);
                }
            }
            else
            {
                GameManager.instance.menuSettings.SoundFail();
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
                myRigidbody.velocity = new Vector2(0, 0);
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
    public void SetMove(bool status)
    {
        move = status;
        myAnimator.SetBool("IsDownWalking", false);
        myAnimator.SetBool("IsUpWalking", false);
        myAnimator.SetBool("IsTurnWalking", false);
    }
}
