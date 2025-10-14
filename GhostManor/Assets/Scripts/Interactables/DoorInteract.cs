using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public FPController player;
    public Item keyType;
    Animator animator;

    public bool isLocked = false;
    bool isOpened = false;

    public void Interact()
    {
        if (isLocked == false)
        {
            if (!isOpened)
            {
                isOpened = true;
                animator.SetBool("Opened", isOpened);
                SoundManager.Play("DoorOpen");
            }
            else
            {
                isOpened = false;
                animator.SetBool("Opened", isOpened);
                SoundManager.Play("DoorOpen");
            }
        }
        else if (isLocked == true)
        {
            if (keyType == player.heldObject)
            {
                isLocked = false; 
                //delete the key
            
            }
            else
            {
                SoundManager.Play("Locked");
            }
            
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
