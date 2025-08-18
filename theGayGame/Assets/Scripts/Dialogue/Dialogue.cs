using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

// 5 Minute DIALOGUE SYSTEM in UNITY Tutorial
// BMo
// 19 March 2021
// Code Version: Unknown
// Available at https://youtu.be/8oTYabhj248?si=Y4JJXjdaxYVkIulq


public class Dialogue : IInteractable
{
    public NPCDialogue dialogueData; //accesses the data
    public TextMeshProUGUI newText; //TMP asset
    public TextMeshProUGUI infoText; //correaltes to the keybind
    public TextMeshProUGUI nameText; //display characters name
    public GameObject dialoguePanel;
    public float textSpeed = 0.02f;
    public static bool isDialogueActive;
    //private int index; //to track what line is what

    public override void Interact()
    {
        StartDialogue();

    }

    void Start()
    {
        dialoguePanel.SetActive(false);

    }


    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        newText.text = string.Empty;
        //newText.color = dialogueData.textColour; will fix this in final
        nameText.SetText(dialogueData.charName);
        dialogueData.index = 0;
        StartCoroutine(TypeLine());
        isDialogueActive = true;

    }

    IEnumerator TypeLine() //This gives the typing effect
    {
    
        foreach (char c in dialogueData.dialogue[dialogueData.index].ToCharArray())
        {
            newText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

     void NextLine() //continues the dialogue
    {
        if (dialogueData.index < dialogueData.dialogue.Length - 1)
        {
            dialogueData.index++;
            newText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
        }
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (newText.text == dialogueData.dialogue[dialogueData.index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                newText.text = dialogueData.dialogue[dialogueData.index]; //and then go to the next line
            }
        }
        if (!context.performed)
        {
            infoText.text = context.control.name + (" to continue");
        }

    }
}
