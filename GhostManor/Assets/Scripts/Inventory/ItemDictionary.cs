using System.Collections.Generic;
using UnityEngine;

// Drag and Drop Inventory UI - Top Down Unity 2D #8
// Game Code Library
// 7 Oct 2024 
// Code Version: Unknown
// Available at: https://youtu.be/wlBJ0yZOYfM?si=3p5aroyykJX-6YqZ

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
            {
                itemPrefabs[i].itemID = i + 1;
            }
        }

        foreach (Item item in itemPrefabs)
        {
            itemDictionary[item.itemID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);

        if (prefab == null)
        {
            Debug.LogWarning($"Item with ID {itemID} not found in dictionary");
        }
        
        return prefab;
    }
}
