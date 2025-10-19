using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public FPController player;
    public HotbarController hotbar;
    public Item keyType;

    private Animator animator;

    public bool isLocked = false;
    bool isOpen = false;

    public void Interact()
    {
        if (isLocked == false)
        {
            if (!isOpen)
            {
                isOpen = true;

                animator.SetBool("isOpen", isOpen);
                SoundManager.Play("DoorOpen");
            }
            else
            {
                isOpen = false;
                
                animator.SetBool("isOpen", isOpen);
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
        animator = GetComponentInParent<Animator>();
    }
}
