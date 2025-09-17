using UnityEngine;

public class CompareObjects : IInteractable
{
    [SerializeField] private ParticleSystem flames;
    public FPController player;
    public Item objectType;
    public GameObject Npc;

    
    public override void Interact()
    {
        CheckHands();
    }

    void CheckHands()
    {
        // What object does the player have in there hands
        if (objectType == player.heldObject)
        {
            Destroy(gameObject);
            Destroy(player.heldObject.gameObject);
        }
        else
        {
            flames.Play(); 
            Debug.Log("Wrong");
        }
    }
}
