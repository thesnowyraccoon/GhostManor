using System.Collections;
using UnityEngine;

// Add NPC and Dialogue System to your Game - Top Down Unity 2D #19
// Game Code Library
// 23 Feb 2025 
// Code Version: Unknown
// Available at: https://youtu.be/eSH9mzcMRqw?si=EnQGNmLLeNjehw7f 

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;

    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public CompareObjects compare;

    private enum CompareState { Correct, Incorrect, NotComparing }
    private CompareState compareState = CompareState.NotComparing;

    // private enum ObjectiveState { NotStarted, InProgress, Completed }
    // private ObjectiveState objectiveState = ObjectiveState.NotStarted;

    void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.isPaused && !isDialogueActive))
        {
            return;
        }

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    public bool IsInteractable()
    {
        return !isDialogueActive;
    }

    void StartDialogue()
    {
        SyncCompareState();

        if (compareState == CompareState.NotComparing)
        {
            dialogueIndex = 0;
        }
        else if (compareState == CompareState.Incorrect)
        {
            dialogueIndex = dialogueData.incorrectItemIndex;
        }
        else if (compareState == CompareState.Correct)
        {
            dialogueIndex = dialogueData.correctItemIndex;
        }

        isDialogueActive = true;

        dialogueUI.SetNPCInfo(dialogueData.npcName);
        dialogueUI.ShowDialogueUI(true);

        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    private void SyncCompareState()
    {
        if (compare != null)
        {
            if (compare.IsComparing() == 0)
            {
                compareState = CompareState.Correct;
            }
            else if (compare.IsComparing() == 1)
            {
                compareState = CompareState.Incorrect;
            }
            else if (compare.IsComparing() == 2)
            {
                compareState = CompareState.NotComparing;
            } 
        }
       
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();

            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();

            return;
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator Typewriter()
    {
        isTyping = true;

        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);

            SoundManager.Play("Dialogue");

            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);

            NextLine();
        }
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(Typewriter());
    }

    public void EndDialogue()
    {
        // if (objectiveState == ObjectiveState.Completed && !ObjectiveController.Instance.isObjHandedIn(dialogueData.objective.objectiveID))
        // {
        //     HandleobjectiveCompletion(dialogueData.objective);
        // }

        StopAllCoroutines();

        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);

        PauseController.SetPause(false);
    }

    // void HandleobjectiveCompletion(Objective objective)
    // {
    //     ObjectiveController.Instance.HandInObjective(objective.objectiveID);
    // }
}
