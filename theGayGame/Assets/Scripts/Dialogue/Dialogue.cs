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
    public NPCDialogue dialogueData;
    public TextMeshProUGUI newText; //TMP asset
    public TextMeshProUGUI infoText; //correaltes to the keybind
    public TextMeshProUGUI nameText; //display characters name
    public GameObject dialoguePanel;
    public float textSpeed = 0.2f;
    private int index; //to track what line is what
    private bool isDialogueActive;

    public override void Interact()
    {
        if (!isDialogueActive)
        {
            StartDialogue();
        }
       
    }

    void Start()
    {

        newText.text = string.Empty;
        newText.color = dialogueData.textColour;
        StartDialogue();
        //Debug.Log("Dialogue Begin");

    }


    void StartDialogue()
    {
        isDialogueActive = true;
        index = 0;
        StartCoroutine(TypeLine());

        nameText.SetText(dialogueData.charName); 
    }

    IEnumerator TypeLine() //This gives the typing effect
    {
        foreach (char c in dialogueData.dialogue[index].ToCharArray())
        {
            newText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

     void NextLine() //continues the dialogue
    {
        if (index < dialogueData.dialogue.Length - 1)
        {
            index++;
            newText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (newText.text == dialogueData.dialogue[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                newText.text = dialogueData.dialogue[index]; //and then go to the next line
            }
        }
        if (!context.performed)
        {
            infoText.text = context.control.name;
        }

    }
}
