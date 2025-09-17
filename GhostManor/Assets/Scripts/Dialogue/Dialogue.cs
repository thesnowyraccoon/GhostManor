using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.Entities.UniversalDelegates;

// 5 Minute DIALOGUE SYSTEM in UNITY Tutorial
// BMo
// 19 March 2021
// Code Version: Unknown
// Available at https://youtu.be/8oTYabhj248?si=Y4JJXjdaxYVkIulq


public class Dialogue : IInteractable
{
    [Header ("INFO")]
    public NPCDialogue dialogueData; //accesses the data
    public GameObject NPC; //the 'sprite'
    public TextMeshProUGUI newText; //TMP asset
    public TextMeshProUGUI infoText; //correaltes to the keybind
    public TextMeshProUGUI nameText; //display characters name
    public GameObject dialoguePanel;

    public GameObject hotbarSlots;
    public float textSpeed = 0.02f;
    public static bool isDialogueActive;
    private int index; //to track what line is what
    public static bool isReceiving = false;


    private enum QuestState { NotStarted, InProgress, Completed }
    private QuestState questState = QuestState.NotStarted;

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
        hotbarSlots.SetActive(false);
        newText.text = string.Empty;
        //newText.color = dialogueData.textColour; will fix this in final
        nameText.SetText(dialogueData.charName);
        StartCoroutine(TypeLine());
        isDialogueActive = true;

        DisplayCurrentLine();

        //Quests
        SyncQuestState();

        if (questState == QuestState.NotStarted)
        {
            index = 0;
        }
        else if (questState == QuestState.InProgress)
        {
            index = dialogueData.questInProgressIndex;
        }
        else if (questState == QuestState.Completed)
        {
            index = dialogueData.questCompletedIndex;
        }
    }

    private void SyncQuestState()
    {
        if (dialogueData.quests == null) return;
        string questID = dialogueData.quests.questID;
        if (QuestController.Instance.IsQuestActive(questID))
        {
            questState = QuestState.InProgress;
        }
        else
        {
            questState = QuestState.NotStarted;
        }
    }

    IEnumerator TypeLine() //This gives the typing effect
    {
        // newText.SetText("");

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
        // if (++index < dialogueData.dialogue.Length)
        // {
        //     StartCoroutine(TypeLine());
        // }
        
        if (dialogueData.endDialogueLines.Length > index && dialogueData.endDialogueLines[index])
        {
            dialoguePanel.SetActive(false);
            hotbarSlots.SetActive(true);
            isDialogueActive = false;
            isReceiving = true;
            return;
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
            infoText.text = context.control.name + " to continue";
        }
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
}
