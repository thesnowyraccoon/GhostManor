using UnityEngine;

public class ButtonInteract : IInteractable
{
    Animator animator;

    public GameObject gameobject;

    public override void Interact()
    {
        animator.SetBool("Pressed", true);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void SetVisible()
    {
        gameobject.SetActive(true);
    }
}
