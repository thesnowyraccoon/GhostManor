using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pillar : MonoBehaviour, IInteractable
{
    [Header("Player")]
    public FPController player;
    public HotbarController hotbar;

    private EndGameController end;

    [Header("Item")]
    public Item objectType;

    [Header("Pillar")]
    public Transform pillarPoint;

    void Start()
    {
        end = GetComponentInParent<EndGameController>();
    }

    public void Interact()
    {
        if (objectType == null) return;

        if (player.heldObject == objectType)
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
        else if (player.heldObject != null && objectType != player.heldObject)
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
