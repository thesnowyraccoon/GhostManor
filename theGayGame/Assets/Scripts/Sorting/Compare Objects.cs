using Unity.VisualScripting;
using UnityEngine;

public class CompareObjects : IInteractable
{
    public FPController player;
    public PickUpObject objectType;
    public GameObject Npc;
    
    public override void Interact()
    {
        CheckHands();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckHands()
    {

        // //What object does the player have in there hands
        if (objectType == player.heldObject)
        {
            Destroy(gameObject);
            Destroy(player.heldObject.gameObject);
        }
        else
        {
            Debug.Log("Wrong");
        }
    }
}
