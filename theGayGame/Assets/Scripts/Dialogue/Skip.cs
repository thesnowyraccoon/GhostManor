using UnityEngine;
using UnityEngine.InputSystem;

public class Skip : MonoBehaviour
{
    public NPCDialogue dialogueData;
    public Dialogue dialogueProcess;
    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (dialogueProcess.newText.text == dialogueData.dialogue[dialogueProcess.index])
            {
               // NextLine(); //accesses the function from the dialogue script
            }
            else
            {
                StopAllCoroutines();
                dialogueProcess.newText.text = dialogueData.dialogue[dialogueProcess.index]; //and then go to the next line
            }
        }
        if (!context.performed)
        {
            dialogueProcess.infoText.text = context.control.name;
        }

    }
}
