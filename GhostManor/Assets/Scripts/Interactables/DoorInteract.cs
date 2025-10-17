using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public FPController player;
    public HotbarController hotbar;
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
                
                hotbar.RemoveItem(player.heldObject.gameObject);
                hotbar.RebuildHotbar();

                Destroy(player.heldObject.gameObject);
                player.heldObject = null;

                //Play unlocked sound
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
