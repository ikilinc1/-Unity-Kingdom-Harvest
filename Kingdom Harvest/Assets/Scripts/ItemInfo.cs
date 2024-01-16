using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Item item;

    public Text itemText;
    public Image itemIcon;

    // Start is called before the first frame update
    private void Start()
    {
        if (item)
        {
            itemIcon.sprite = item.icon;
            itemText.text = item.placeableAmount.ToString();
        }
    }

    public void SetButtonInfo(Item initItem)
    {
        this.item = initItem;
        itemIcon.sprite = initItem.icon;
        itemText.text = initItem.placeableAmount.ToString();
    }

    
    public void RefreshAmount()
    {
        if (item)
        {
            itemText.text = item.placeableAmount.ToString();
            if (item.placeableAmount == 0)
            {
                DestroyImmediate(this.gameObject, true);
            }
        }
        
    }

    public void DestroyItemInfo()
    {
        DestroyImmediate(this.gameObject,true);
    }
    
    public void StartPlacing()
    {
        // do not forget to update inventory after placing stops or escape is clicked !!!!!!!!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!
        // also endTurn does not delete existing inventory items after the first end turn
        ItemPlacer.Instance.PlaceItemStart(item.id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
