using UnityEngine;

public class DoorInteract : IInteractable
{
    Animator animator;

    bool isOpened = false;

    public override void Interact()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("Opened", isOpened);
        }
        else
        {
            isOpened = false;
            animator.SetBool("Opened", isOpened);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
