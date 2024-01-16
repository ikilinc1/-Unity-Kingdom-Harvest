using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MapTile {
    public Vector2 location ;
    public Item item;
    public GameObject itemPrefab;

    public MapTile(Vector2 location, Item item, GameObject prefab)
    {
        this.location = location;
        this.item = item;
        this.itemPrefab = prefab;
    }
}



public class MapPartPlacementInfo : MonoBehaviour
{
    //asagiya inmek icin y -1
    // saga gitmek icin x +1
    // x en kucuk -2, en buyuk 12
    // y en buyuk 2 en kucuk -12
    // tikladiginda buldugun kordinatlari check et ve clample
    // obje yerlestiriyorsan da kaydet
    // her obje yerlestirme emri verildiginde mouse un tikladigi yeri check et
    // Start is called before the first frame update

    public static MapPartPlacementInfo Instance;
    
    public List<MapTile> mapTiles;

    private void Start()
    {
        mapTiles = new List<MapTile> { };
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

    public bool RegisterItemLocation(Vector3 position, Item item, GameObject prefab)
    {
        mapTiles.Add(new MapTile(new Vector2(position.x, position.y), item, prefab));
        return true;
    }
    
    public bool IsSpaceEmpty(Vector3 position, Item item)
    {
        return IsSpaceEmpty(position);
    }
    
    public void RunAnimations()
    {
        for (int i = 0; i < mapTiles.Count; i++)
        {
            mapTiles[i].itemPrefab.GetComponent<Animator>().SetTrigger("EndTurnTrigger");
            mapTiles[i].itemPrefab.GetComponent<EndTurnAnimationStarter>().PlayAnimation();
        }
    }

    private bool IsSpaceEmpty(Vector3 position)
    {
        for (int i = 0; i < mapTiles.Count; i++)
        {
            if (mapTiles[i].location.x == position.x && mapTiles[i].location.y == position.y )
            {
                return false;
            }
        }

        return true;
    }
}
