using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    [SerializeField] private FPController player;

    public GameObject hotBarPanel;

    public void AddItem(GameObject item)
    {
        foreach (Transform slotTransform in hotBarPanel.transform)
        {
            HotbarSlot slot = slotTransform.GetComponent<HotbarSlot>();

            if (slot != null && slot.currentItem == null)
            {
                slot.currentItem = item;

                return;
            }
        }
    }

    public void RemoveItem(GameObject item)
    {
        foreach (Transform slotTransform in hotBarPanel.transform)
        {
            HotbarSlot slot = slotTransform.GetComponent<HotbarSlot>();

            if (slot != null && slot.currentItem != null)
            {
                if (item == slot.currentItem)
                {
                    slot.currentItem = null;

                    return;
                }
            }
        }
    }

    void OpenItemSlot(int index)
    {
        if (player.heldObject != null)
        {
            player.heldObject = null;

            foreach (Transform item in player.holdPoint)
            {
                item.gameObject.SetActive(false);
            }
        }

        if (player.holdPoint.GetChild(index).TryGetComponent<Item>(out var heldItem))
            heldItem.gameObject.SetActive(true);

        player.heldObject = heldItem;
    }

    public void OnHotBar1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OpenItemSlot(0);
    }

    public void OnHotBar2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OpenItemSlot(1);
    }

    public void OnHotBar3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OpenItemSlot(2);
    }

    public void OnHand(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        foreach (Transform item in player.holdPoint)
        {
            item.gameObject.SetActive(false);
        }

        player.heldObject = null;
    }

    public void RebuildHotbar()
    {
        foreach (Transform slotTransform in hotBarPanel.transform)
        {
            HotbarSlot slot = slotTransform.GetComponent<HotbarSlot>();

            if (slot != null && slot.currentItem != null)
            {
                slot.currentItem = null;
            }
        }

        foreach (Transform item in player.holdPoint)
        {
            AddItem(item.gameObject);
        }
    }
}