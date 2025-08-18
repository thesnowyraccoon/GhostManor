using UnityEngine;

public class CompareObjects : IInteractable
{
    public Rigidbody objectType;
    public GameObject Npc;
    public override void Interact()
    {
        //CheckHands()
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
        //What object does the player have in there hands
        /*if (object in holdpoint = obectType)
        {
            Destroy.Npc;
        }
        else
        {
            Debug.Log("Wrong");
        }*/ 
    }
}
