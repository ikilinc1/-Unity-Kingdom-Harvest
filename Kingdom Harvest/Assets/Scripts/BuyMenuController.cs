using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuController : MonoBehaviour
{
    public GameObject currentMapPart;  // Drag your map part prefab to this field in the Inspector
    public Transform player;          // Drag your player GameObject to this field in the Inspector

    private bool isActive;

    private void Start()
    {
        isActive = false;
    }

    public void OpenBuyMenu()
    {
        currentMapPart = player.GetComponent<PlayerController>().mapParts.GetCurrentMapPart();
        List<string> directions = player.GetComponent<PlayerController>().mapParts.BuyableDirections();
        List<Button> buttons = currentMapPart.GetComponent<MapPartBuyButtons>().GetButtons();
        Debug.Log(1);
        if (!isActive)
        {
            if (directions.Contains("top") )
            {
                Debug.Log(2);
                buttons[0].gameObject.SetActive(true);
            }
            if (directions.Contains("bottom") )
            {
                Debug.Log(3);
                buttons[1].gameObject.SetActive(true);
            }
            if (directions.Contains("right") )
            {
                Debug.Log(4);
                buttons[2].gameObject.SetActive(true);
            }
            if (directions.Contains("left"))
            {
                Debug.Log(5);
                buttons[3].gameObject.SetActive(true);
            }
            isActive = true;
        }
        
    }

    public void CloseBuyMenu()
    {
        currentMapPart = player.GetComponent<PlayerController>().mapParts.GetCurrentMapPart();
        List<Button> buttons = currentMapPart.GetComponent<MapPartBuyButtons>().GetButtons();
        if (isActive)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            isActive = false;
        }
    }
    public void BuyLand(string direction)
    {
        currentMapPart = player.GetComponent<PlayerController>().mapParts.GetCurrentMapPart();
        List<Button> buttons = currentMapPart.GetComponent<MapPartBuyButtons>().GetButtons();
        bool goldCheck = player.GetComponent<PlayerController>().IsGoldEnoughToBuy();
        if (direction == "top" && goldCheck)
        {
            buttons[0].gameObject.SetActive(false);
            player.GetComponent<PlayerController>().HandleMapExpansion(Vector2.up);
            this.CloseBuyMenu();
        }
        if (direction == "bottom" && goldCheck)
        {
            buttons[1].gameObject.SetActive(false);
            player.GetComponent<PlayerController>().HandleMapExpansion(Vector2.down);
            this.CloseBuyMenu();
        }
        if (direction == "right" && goldCheck)
        {
            buttons[2].gameObject.SetActive(false);
            player.GetComponent<PlayerController>().HandleMapExpansion(Vector2.right);
            this.CloseBuyMenu();
        }
        if (direction == "left" && goldCheck)
        {
            buttons[3].gameObject.SetActive(false);
            player.GetComponent<PlayerController>().HandleMapExpansion(Vector2.left);
            this.CloseBuyMenu();
        }
    }
}