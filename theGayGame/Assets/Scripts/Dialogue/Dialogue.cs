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

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI newText; //TMP asset
    public TextMeshProUGUI infoText; //correaltes to the keybind
    public float textSpeed;
    private int index; //to track what line is what
    public string[] dialogue; //actaul words
    public Color textColour;


    void Start()
    {
        
        newText.text = string.Empty;
        newText.color = textColour;
        StartDialogue();
    
    }


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() //This gives the typing effect
    {
        foreach (char c in dialogue[index].ToCharArray())
        {
            newText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() //continues the dialogue
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            newText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (newText.text == dialogue[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                newText.text = dialogue[index]; //and then go to the next line
            }
        }
        if (!context.performed)
        {
            infoText.text = context.control.name;
        }
       
    }
}
