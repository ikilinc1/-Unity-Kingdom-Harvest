using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllItemsController : MonoBehaviour
{
    // Singleton instance
    public static AllItemsController Instance;
    
    public List<Item> tier_1;
    public List<Item> tier_2;
    public List<Item> tier_3;
    public List<Item> tier_4;
    public List<Item> tier_5;

    public PlayerController playerController;

    public GameObject buttonPrefab;
    public Text goldText;

    public int taxAmount;

    private int turnCounter;
    
    //Later add total sell amount and calculate at the end turn function
    // and think all sell possibilities later !!!!
    public int totalSellAmount;
    // Start is called before the first frame update
    void Start()
    {
        turnCounter = 1;
        taxAmount = 10;
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
    // !!!! every end turn should calculate inventory
    // calculates the placed amount and new placeable amount correct but something is wrong with
    public void EndTurnCalculations()
    {
        //endturn started disable buttons and inventory access
        
        // !! need to add a condition to sell first turn without placing anything

        if (turnCounter %3 == 0)
        {
            // pay taxes
            // game over if player cannot pay

            // needs balancing
            taxAmount = taxAmount * taxAmount;
        }
        
        int endTurnGold = 0;
        
        List<GameObject> newPrefabButtons = new List<GameObject>() { };
        for (int i = 0; i < tier_1.Count; i++)
        {
            // Sell stuff here !!!!!!!
            
            PlayerController.Instance.AddGold(tier_1[i].placeableAmount * tier_1[i].outputSellValue);

            goldText.text = PlayerController.Instance.gold.ToString();
            endTurnGold += tier_1[i].placeableAmount * tier_1[i].outputSellValue;
            this.AddToItem(tier_1[i]);
            if (tier_1[i].placeableAmount > 0)
            {
                // we should maybe create the buttons here and pass it to player controller maybe ?
                // yea we should initiate ItemInfo cleaner because we are trying to access it using refreshinventory
                // and also initiateInventory
                
                // wrong code probably here
                //instead create the prefab button here and send it to InitiateInventory
                GameObject tmpButtonPrefab = Instantiate(buttonPrefab);
                tmpButtonPrefab.GetComponent<ItemInfo>().SetButtonInfo(tier_1[i]);
                newPrefabButtons.Add(tmpButtonPrefab);
            }
           
        }
        
        playerController.ResetInventory();
        // send List<GameObject> buttons
        playerController.InitiateInventory(newPrefabButtons);
        // Here refresh inventory
        playerController.RefreshInventory();
        MapPartPlacementInfo.Instance.RunAnimations();
        turnCounter++;
        
        // Here add text next to gold counter to indicate end turn gold earnings
        PlayerController.Instance.EndTurnGoldTextAndAnimationTrigger(endTurnGold);
        //endturn ended enable buttons and inventory access
    }
    
    public void OpenInventoryRefresh()
    {
        
        // get current buttons
        // delete 0 amounts
        // update otherwise with new button
    }
    
    public void AddToItem(Item item)
    {
        Item tmp = this.GetItemFromId(item.outputId);
        // add field info here later
        int amount = item.standardOutputAmount * item.placedAmount;
        tmp.placeableAmount = amount;
    }
    
    // get random three, probability calculations for tiers
    
    // update item counts as well
    
    //get item count for a specific item
    
    // get item from id
    public Item GetItemFromId(int id)
    {
        int absoluteValue = Mathf.Abs(id);

        if (absoluteValue >= 10 && absoluteValue <= 99)
        {
            for (int i = 0; i < tier_1.Count; i++)
            {
                if (tier_1[i].id == id)
                {
                    return tier_1[i];
                }
            }
        }
        return null;
    }
}
