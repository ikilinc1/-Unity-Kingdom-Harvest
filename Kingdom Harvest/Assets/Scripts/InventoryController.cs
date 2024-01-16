using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    
    public List<ItemInfo> itemInfos;

    public GameObject content;
    public GameObject prefabButton;
    // Start is called before the first frame update
    void Start()
    {
        
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
    
    public void ResetItemInfoList()
    {
        //destroy existing gameObjects here
        for (int i = 0; i < itemInfos.Count; i++)
        {
            if (itemInfos[i])
            {
                DestroyImmediate(itemInfos[i].gameObject);
            }
            
        }
        itemInfos = new List<ItemInfo>(){};
    }
    
    public void AddToItemListAndInitiateInventory(List<GameObject> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            itemInfos.Add(buttons[i].GetComponent<ItemInfo>());
            //Cannot set itemInfo properly !!!!
            // initiate list[i]
            //GameObject newButton = Instantiate(buttons[i]);
            // add to content
            buttons[i].transform.SetParent(content.transform,false);
        }
    }

    public void RefreshAllItemInfo()
    {
        for (int i = 0; i < itemInfos.Count; i++)
        {
            if (itemInfos[i])
            {
                itemInfos[i].RefreshAmount();
                // change the button as well
               // itemInfos[i].itemText.text = "lol oluyormus burada";
            }
            else
            {
                Debug.Log("Questinable here !!!!");
                itemInfos.RemoveAt(i);
            }
        }
    }
    
    public void RefreshSelectedInventory(int id)
    {
        for (int i = 0; i < itemInfos.Count; i++)
        {
            if (itemInfos[i].item.id == id)
            {
                Debug.Log("in loop at refresh selected inventory");
                Debug.Log(itemInfos[i].item.placeableAmount);
                itemInfos[i].RefreshAmount();
                //itemInfos[i].gameObject.GetComponentInChildren<Text>().GetComponent<Text>().text = itemInfos[i].item.placeableAmount.ToString();
                // itemInfos[i].itemText.text = itemInfos[i].item.placeableAmount.ToString();
                if (itemInfos[i].item.placeableAmount == 0)
                {
                    //itemInfos[i].gameObject.SetActive(false);
                }
                // maybe change the prefab here as well
            }
            
        }
    }
}
