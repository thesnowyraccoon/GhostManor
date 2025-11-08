using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public FPController player;
    public HotbarController hotbar;
    public int keyID;

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
            if (keyID == player.heldObject.itemID)
            {
                isLocked = false;

                GameObject held = player.heldObject.gameObject;

                player.heldObject.Drop();
                player.heldObject = null;

                hotbar.RemoveItem(held);

                Destroy(held);
                
                hotbar.RebuildHotbar();

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
