using UnityEngine;

public class Hide : IInteractable 
{
    public FPController player;
    private float height;
    public float hideHeight = 0.5f;
    private bool isHiding = false;

    public override void Interact()
    {

        if (!isHiding)
        {
            isHiding = true;
            height = hideHeight;
            //transform player position
        }
        else
        {
            isHiding = false;
            height = player.standHeight;
            //transform player position
        }
        
    }
  
}
