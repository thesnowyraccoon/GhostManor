using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> items;

    void Awake()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                items[i].itemID = i + 1;
            }
        }
    }
}
