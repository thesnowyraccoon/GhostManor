using UnityEngine;

public class CompareObjects : MonoBehaviour
{
    public FPController player;
    public Item objectType;
    public NPC npc;

    void CheckHands()
    {
        Destroy(player.heldObject.gameObject);

        // // What object does the player have in there hands
        // if (objectType == player.heldObject)
        // {
        //     //Destroy(gameObject);
        //     Destroy(player.heldObject.gameObject);
        // }
        // else
        // {
        //     //play particle, throw object
        //     //Debug.Log("Wrong");
        // }
    }

    public int IsComparing()
    {
        if (objectType == player.heldObject)
        {
            CheckHands();

            return 0;
        }
        else if (objectType != null && objectType != player.heldObject)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
