using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pillar : MonoBehaviour, IInteractable
{

    public FPController player;
    public Item objectType;
    public HotbarController hotbar;
    public Transform pillarPoint;
    public static bool hasItem = false;

     public void Interact()
    {
        if (player.heldObject == objectType)
        {
            SoundManager.Play("Correct");

            Item item = player.heldObject;
            player.heldObject.Drop();
            player.heldObject = null;

            hotbar.RebuildHotbar();

            item.PickUp(pillarPoint);
            item.MoveToHoldPoint(pillarPoint.position);

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
