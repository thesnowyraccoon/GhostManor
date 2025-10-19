using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButtonInteract : MonoBehaviour, IInteractable
{
    Animator animator;

    public GameObject gameobject;

    public void Interact()
    {
        animator.SetBool("isPressed", true);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetVisible()
    {
        gameobject.SetActive(true);
    }
}
