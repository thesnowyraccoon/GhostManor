using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    Animator animator;

    public GameObject gameobject;

    public void Interact()
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
