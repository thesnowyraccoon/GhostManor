using Unity.VisualScripting;
using UnityEngine;

public class Pillar : MonoBehaviour, IInteractable
{
    [Header("Player")]
    public FPController player;
    public HotbarController hotbar;

    private EndGameController end;

    [Header("Item")]
    public int itemID;

    [Header("Pillar")]
    public Transform pillarPoint;

    void Start()
    {
        end = GetComponentInParent<EndGameController>();
    }

    public void Interact()
    {
        if (itemID == 0) return;

        if (player.heldObject.itemID == itemID)
        {
            SoundManager.Play("Correct");

            Item item = player.heldObject;

            player.heldObject.Drop();
            player.heldObject = null;

            hotbar.RebuildHotbar();

            item.PickUp(pillarPoint);
            item.MoveToHoldPoint(pillarPoint.position);

            end.CheckEnd();
        }
        else if (player.heldObject != null && itemID != player.heldObject.itemID)
        {
            SoundManager.Play("Wrong");
            Debug.Log("Wrong Item");
        }
        else if (player.heldObject == null)
        {
            Debug.Log("Hands Empty");
        }
    }
}
