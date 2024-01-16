using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPartBuyButtons : MonoBehaviour
{
    public List<Button> buttons;
    public BuyMenuController buyMenuController;

    private void Start()
    {
        buyMenuController = GameObject.Find("BuyMenu").GetComponent<BuyMenuController>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buyMenuController.CloseBuyMenu();
        }
    }

 

    public List<Button> GetButtons()
    {
        return buttons;
    }

    public void BuyLand(string direction)
    {
        buyMenuController.BuyLand(direction);   
    }
}
