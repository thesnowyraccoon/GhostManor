using UnityEngine;

[RequireComponent(typeof(NPC))]
public class Pillar : MonoBehaviour
{

    [Header("Player")]
    public FPController player;
    public Item objectType;
    public HotbarController hotbar;
    public GameObject pillarPoint; 


    void Start()
    {
        
    }

    void CheckHands()
    {
        SoundManager.Play("Correct");
        //player.heldObject.MoveToPillar(pillarPoint.position);
        
        player.heldObject = null;

        hotbar.RebuildHotbar();

        
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
            SoundManager.Play("Wrong");
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
