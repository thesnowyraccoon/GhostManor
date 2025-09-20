using UnityEngine;

public class Hide : MonoBehaviour, IInteractable 
{
    private FPController playerController;
    private CharacterController controller;
    public GameObject player;
    public float hideHeight = 0.5f;
    private bool isHiding = false;
    public GameObject hidingSpot;
    public GameObject exitSpot;

    void Start()
    {
        playerController = player.GetComponent<FPController>();
        controller = player.GetComponent<CharacterController>();
        
    }

    public void Interact()
    {

        if (!isHiding)
        {
            isHiding = true;
            controller.height = hideHeight;
            player.transform.position = hidingSpot.GetComponent<Transform>().position;
        }
        else
        {
            isHiding = false;
            controller.height = playerController.standHeight;
            player.transform.position = exitSpot.GetComponent<Transform>().position;
        }

    }
  
}
