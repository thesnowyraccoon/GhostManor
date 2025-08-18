using TMPro;
using UnityEngine;

public class HotbarSlot : MonoBehaviour
{
    public GameObject currentItem;

    private TextMeshProUGUI itemName;

    void Start()
    {
        itemName = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (itemName != null)
        {
            if (currentItem != null)
            {
                itemName.text = currentItem.GetComponent<Item>().itemName;
            }
            else
            {
                itemName.text = null;
            }
        }
    }
}
