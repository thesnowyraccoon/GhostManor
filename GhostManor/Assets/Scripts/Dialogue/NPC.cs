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

    private enum ObjectiveState { NotStarted, InProgress, Completed }
    private ObjectiveState objectiveState = ObjectiveState.NotStarted;

    void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
