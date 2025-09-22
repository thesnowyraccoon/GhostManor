using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    Animator animator;

    bool isOpened = false;

    public void Interact()
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

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
