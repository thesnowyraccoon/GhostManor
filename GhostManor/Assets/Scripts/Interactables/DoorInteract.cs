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

                GameObject item = player.heldObject.gameObject;

                player.heldObject.Drop();

                hotbar.RemoveItem(item);
                hotbar.RebuildHotbar();

                item.SetActive(false);

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
