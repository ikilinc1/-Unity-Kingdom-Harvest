using System;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    // Singleton instance
    public static ItemPlacer Instance;
    
    public MapParts mapParts;
    public GameObject itemPrefab;
    public GameObject previewPrefab; // Assign a preview item prefab in the Inspector
    public float gridSize = 1.0f;
    public LayerMask groundLayer; // Assign the Ground layer in the Inspector
    public Item selectedItem;
    public int selectedItemCount;
    public PlayerController playerController;
    
    private GameObject previewItem;

    private AllItemsController allItems;

    private bool isPlacing;

    private void Start()
    {
        isPlacing = false;
        allItems = GameObject.Find("AllItems").GetComponent<AllItemsController>();
    }
    
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

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the position in the grid
        Vector3 gridPosition = CalculateGridPosition(mousePosition);

        // Check if the grid position is on the ground layer
        bool isOnGroundLayer = IsOnGroundLayer(gridPosition);

        if (isPlacing)
        {
            // Update or remove the preview item
            UpdatePreviewItem(isOnGroundLayer, gridPosition);
            if (selectedItemCount > 0 )
            {
                // Check if the left mouse button is clicked
                if (Input.GetMouseButtonDown(0) && isOnGroundLayer && IsSpaceEmpty(gridPosition))
                {
                    // Place the actual item at the calculated grid position
                    PlaceItem(gridPosition);

                    // Remove or replace the preview item if it exists
                    if (previewItem != null)
                    {
                        Destroy(previewItem);
                        previewItem = null;
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(previewItem);
                previewItem = null;
                // not refresh all, refresh current itemInfo!!!!!
                //PlayerController.Instance.RefreshSelectedInventory(selectedItem.id);
                InventoryController.Instance.RefreshSelectedInventory(selectedItem.id);
                isPlacing = false;
                PlayerController.Instance.isPlacing = false;
                
               
                
            }
        }
        
        
        
        
    }

    Vector3 CalculateGridPosition(Vector3 position)
    {
        // Round the position to the nearest grid cell
        float xGrid = Mathf.Round(position.x / gridSize) * gridSize;
        float yGrid = Mathf.Round(position.y / gridSize) * gridSize;

        return new Vector3(xGrid, yGrid, 0f);
    }

    bool IsOnGroundLayer(Vector3 position)
    {
        // Perform a raycast to check if the position is on the ground layer
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f, groundLayer);
        return hit.collider != null;
    }

    bool IsSpaceEmpty(Vector3 position)
    {
        MapPartPlacementInfo mapInfo = MapPartPlacementInfo.Instance;
       // Here as to current map part
       //return mapInfo.RegisterItemLocation(position, selectedItem);
       return mapInfo.IsSpaceEmpty(position, selectedItem);
    }

    void UpdatePreviewItem(bool isOnGroundLayer, Vector3 position)
    {
        if (isOnGroundLayer)
        {
            // If the preview item doesn't exist, instantiate it
            if (previewItem == null)
            {
                previewItem = Instantiate(previewPrefab, position, Quaternion.identity);
            }
            else
            {
                // Update the position of the existing preview item
                previewItem.transform.position = position;
            }
        }
        else
        {
            // Remove or replace the preview item if it exists
            if (previewItem != null)
            {
                Destroy(previewItem);
                previewItem = null;
            }
        }
    }

    void PlaceItem(Vector3 position)
    {
        // Instantiate the actual item prefab at the specified position
        GameObject initiatedPrefabToStored = Instantiate(itemPrefab, position, Quaternion.identity);
        selectedItemCount--;
        // should also decrease from the Item
        selectedItem.placeableAmount--;
        selectedItem.placedAmount++;
        if (selectedItemCount == 0)
        {
            // why not working!!!! Make inventory singleton maybe ?
            InventoryController.Instance.RefreshSelectedInventory(selectedItem.id);
            isPlacing = false;
            PlayerController.Instance.isPlacing = false;
            // refresh inventory items here
            
        }
        MapPartPlacementInfo.Instance.RegisterItemLocation(position, selectedItem, initiatedPrefabToStored);
    }

    void OnDrawGizmos()
    {
        // Draw a ray for debugging
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * gridSize);

        // Draw a wireframe circle for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gridSize / 2.0f);
    }
    
    
    
    // Button clicked from inventory here
    public void PlaceItemStart(int selectedItemId)
    { 
   
        Item selectedItemFromAllItems = AllItemsController.Instance.GetItemFromId(selectedItemId);

        if (selectedItemFromAllItems.isPlaceable)
        {
            // all should come from the item but for some reason we cannot get these
            itemPrefab = selectedItemFromAllItems.itemPrefab;
            previewPrefab = selectedItemFromAllItems.previewPrefab;
            selectedItem = selectedItemFromAllItems;
            selectedItemCount = selectedItemFromAllItems.placeableAmount;
    
            //!!
            //start placing here or set some bool to start from update
            // also close the inventory as well
            isPlacing = true;
            PlayerController.Instance.ManageInventoryUI();
            PlayerController.Instance.isPlacing = true;
        }
    }
}
