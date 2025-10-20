using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class CompareObjects : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem flames;
    public ParticleSystem stars;

    [Header("Player")]
    public FPController player;
    public Item objectType;
    public HotbarController hotbar;

    [Header("NPC")]
    public NPC npc;

    void Start()
    {
        npc = GetComponent<NPC>();
    }

    void CheckHands()
    {
        stars.Play();
        SoundManager.Play("Correct");

        GameObject item = player.heldObject.gameObject;

        player.heldObject.Drop();

        hotbar.RemoveItem(item);
        hotbar.RebuildHotbar();

        item.SetActive(false);

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
            SoundManager.Play("Wrong");

            player.heldObject.Drop();

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
