using UnityEngine;

public class CompareObjects : MonoBehaviour
{
    public ParticleSystem flames;
    public ParticleSystem stars;
    public FPController player;
    public Item objectType;
    public NPC npc;

    void CheckHands()
    {
        stars.Play();
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
        else if (player.heldObject != null && objectType != player.heldObject)
        {
            flames.Play();
            return 1;
        }
        else if (player.heldObject == null)
        {
            Debug.Log("Hands Empty");
            return 2;
        }
        else
        {
            return 3;
        }
        
    }
}
