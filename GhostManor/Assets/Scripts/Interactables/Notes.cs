using UnityEngine;

public class Notes : IInteractable
{
    public GameObject note;
    public GameObject hotBar;
    private bool isReading = false;

    void Start()
    {
        note.SetActive(false);
    }
    public override void Interact()
    {
        if (!isReading)
        {
            hotBar.SetActive(false);
            note.SetActive(true);
            isReading = true;
            SoundManager.Play("Paper");
        }
        else
        {
            
            hotBar.SetActive(true);
            note.SetActive(false);
            isReading = false;
            SoundManager.Play("Paper");
        }
    }
}
