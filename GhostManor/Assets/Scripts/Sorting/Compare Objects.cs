using UnityEngine;

public class CompareObjects : MonoBehaviour, IInteractable
{
    public FPController player;
    public Item objectType;
    public GameObject Npc;


    public void Interact()
    {
        //     CheckHands();
    }

    // void CheckHands()
    // {
        
    //     // What object does the player have in there hands
    //     if (objectType == player.heldObject)
    //     {
    //         Destroy(gameObject);
    //         Destroy(player.heldObject.gameObject);
    //     }
    //     else
    //     {
    //         //play particle, throw object
    //         //Debug.Log("Wrong");
    //     }
    // }
}
