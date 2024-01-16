using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    
    public string itemName;

    public int tier;

    public int standardOutputAmount;

    public int outputId;
    
    public string outputName;

    public int outputSellValue;

    public int placeableAmount;

    public int placedAmount;
    
    public Sprite icon;

    public Sprite outputIcon;

    public bool isPlaceable;

    public bool isFinalItem;

    public List<int> rawMaterialIds;

    public List<int> rawMaterialAmounts;
    
    public GameObject itemPrefab;
    
    public GameObject previewPrefab;
    
    // later maybe add effects here as well
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
