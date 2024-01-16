using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Singleton instance
    public static PlayerController Instance;
    
    public float moveSpeed = 5f;
    public float characterScaleFactor = 1.0f;
    
    public int gold = 0;
    public Text goldText; // Reference to the UI Text element
    
    public int landCost = 10;
    
    public GameObject mapPartPrefab; // Reference to the map part prefab
    public Transform mapParent; // Parent transform for instantiated map parts

    public MapParts mapParts;

    public Canvas inventory;
    public InventoryController inventoryController;

    public Text endTurnGoldEarningsText;

    public bool isPlacing;
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isInventoryOpen;

    void Awake()
    {
        // Ensure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isInventoryOpen = false;
        isPlacing = false;
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.I) && !isPlacing)
        {
           // AllItemsController.Instance.OpenInventoryRefresh();
            ManageInventoryUI();
            // hmm maybe we can update here using end turn without calculations?
            //
        }
    }

    

    public bool IsGoldEnoughToBuy()
    {
        return gold >= landCost;
    }
    
    public void HandleMapExpansion(Vector2 direction)
    {
        int expansionCost = CalculateExpansionCost(); // Implement this method
        if (gold >= expansionCost)
        {
            // Deduct gold and expand the map
            AddGold(-1*expansionCost);
            ExpandMap(direction);
        }
        else
        {
            // Display a message or perform other actions if the player doesn't have enough gold
        }
    }
    
    int CalculateExpansionCost()
    {
        // Implement logic to calculate the cost based on your game's requirements
        int expansionCost = landCost;
        landCost = landCost * 2;
        return expansionCost; // Example cost
    }
    
    void ExpandMap(Vector2 direction)
    {
        // need current map location
        Vector2 basePosition = mapParts.GetCurrentMapPart().transform.position ;
        direction.x = direction.x * 16 + basePosition.x;
        direction.y = direction.y * 16 + basePosition.y;
        // Implement logic to expand the map based on your game's requirements
        // This may involve instantiating new tiles or adjusting existing ones
        // Instantiate the map part prefab at the specified position
        GameObject newMapPart = Instantiate(mapPartPrefab, new Vector3(direction.x, direction.y, mapPartPrefab.transform.position.z), Quaternion.identity);
        
        // Optionally, you can parent the new map part to a container
        if (mapParent != null)
        {
            List<string> joinedString = mapParts.BuyableDirections();
            
            newMapPart.transform.parent = mapParent;
            mapParts.mapParts.Add(newMapPart);

        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        
        

        // Cast a ray from the new position to check the layer
        RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.zero, 0f);

        
        // Check if the hit object is on the "Ground" layer
        if (!mapParts.IsWalkable(newPosition))
        {
            return;
            
        }
        transform.position = newPosition;
        //rb.MovePosition(newPosition);
        //rb.velocity = moveDirection * moveSpeed;

        // Flip the sprite horizontally if moving left
        if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Flip the sprite back to its original direction if moving right
        else if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Adjust the character sprite size
        AdjustCharacterSize();
    }
    
    public void ManageInventoryUI()
    {
       inventory.gameObject.SetActive(!isInventoryOpen);
       isInventoryOpen = !isInventoryOpen;
    }

    public void RefreshInventory()
    {
        inventoryController.RefreshAllItemInfo();
    }
    
    public void RefreshSelectedInventory(int id)
    {
        inventoryController.RefreshSelectedInventory(id);
    }
    
    public void ResetInventory()
    {
        inventoryController.ResetItemInfoList();
    }
    
    public void InitiateInventory(List<GameObject> buttons)
    {
        inventoryController.AddToItemListAndInitiateInventory(buttons);
    }

    private void UpdateAnimation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetBool("IsWalking", moveDirection != Vector2.zero);
    }
    
    public void EndTurnGoldTextAndAnimationTrigger(int endTurnGoldEarnings)
    {
        // update text
        Debug.Log(11111);
        endTurnGoldEarningsText.text = "+" + endTurnGoldEarnings;
        // trigger animation
        endTurnGoldEarningsText.gameObject.GetComponent<Animation>().Play("EndTurnGoldEarningsAnim");
    }

    private void AdjustCharacterSize()
    {
        // Adjust the character's sprite size based on the characterScaleFactor
        transform.localScale = new Vector3(characterScaleFactor, characterScaleFactor, 1f);
    }
    
    public void AddGold(int amount)
    {
        gold += amount;

        // Update UI Text
        if (goldText != null)
            goldText.text = gold.ToString();

        // Optionally: Play a sound, show a particle effect, etc.
    }
}