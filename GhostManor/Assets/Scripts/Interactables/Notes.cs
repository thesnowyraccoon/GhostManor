using UnityEngine;

public class Notes : IInteractable
{
    public GameObject note;
    private bool isReading = false;

    void Start()
    {
        note.SetActive(false);
    }
    public override void Interact()
    {
        if (!isReading)
        {
            note.SetActive(true);
            isReading = true;
            SoundManager.Play("Paper");
        }
        else
        {
            note.SetActive(false);
            isReading = false;
            SoundManager.Play("Paper");
        }
    }
}
