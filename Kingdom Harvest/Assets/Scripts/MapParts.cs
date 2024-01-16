using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParts : MonoBehaviour
{
    public List<GameObject> mapParts;
    public int currentMapPartIndex;
    public LayerMask itemLayer;

    private void Start()
    {
        currentMapPartIndex = 0;
    }

    public GameObject GetCurrentMapPart()
    {
        return mapParts[currentMapPartIndex];
    }
    
    public bool IsWalkable(Vector2 newPosition)
    {
        // Implement logic to calculate the cost based on your game's requirements
        for (int i = 0; i < mapParts.Count; i++)
        {
            if (mapParts[i].GetComponent<BoxCollider2D>().bounds.Contains(newPosition))
            {
                currentMapPartIndex = i;
                return true;
            }
            
        }
        return false;
    }
    
    public List<string> BuyableDirections()
    {
        mapParts[currentMapPartIndex].GetComponent<BoxCollider2D>().enabled = false;
        List<string> directions = new List<string>();
        Vector2 currentPosition = mapParts[currentMapPartIndex].transform.position;
        currentPosition = new Vector2(currentPosition.x + 5.25f, currentPosition.y - 4.5f);
        RaycastHit2D top = Physics2D.Raycast(currentPosition, Vector2.up, 11f, itemLayer);  // Adjust the distance as needed
        RaycastHit2D bottom = Physics2D.Raycast(currentPosition, Vector2.down, 11f,itemLayer);  // Adjust the distance as needed
        RaycastHit2D right = Physics2D.Raycast(currentPosition, Vector2.right, 11f,itemLayer);  // Adjust the distance as needed
        RaycastHit2D left = Physics2D.Raycast(currentPosition, Vector2.left, 11f,itemLayer);  // Adjust the distance as needed
        mapParts[currentMapPartIndex].GetComponent<BoxCollider2D>().enabled = true;
        Debug.DrawRay(currentPosition, Vector2.up * 11f, Color.green, 10f);
        Debug.DrawRay(currentPosition, Vector2.down * 11f, Color.green, 10f);
        Debug.DrawRay(currentPosition, Vector2.right * 11f, Color.green, 10f);
        Debug.DrawRay(currentPosition, Vector2.left * 11f, Color.green, 10f);
        if(top.collider == null){directions.Add("top");}
        if(bottom.collider == null){directions.Add("bottom");}
        if(right.collider == null){directions.Add("right");}
        if(left.collider == null){directions.Add("left");}
        
        return directions;
    }
}
