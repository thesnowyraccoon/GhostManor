using System.Collections;
using UnityEngine;

// Add NPC and Dialogue System to your Game - Top Down Unity 2D #19
// Game Code Library
// 23 Feb 2025 
// Code Version: Unknown
// Available at: https://youtu.be/eSH9mzcMRqw?si=EnQGNmLLeNjehw7f 

public class NPC : MonoBehaviour, IInteractable
{
    [Header("NPC Data")]
    public NPCDialogue dialogueData;
    public Transform holdPoint;

    [Header("Particles")]
    public ParticleSystem flames;
    public ParticleSystem stars;

    public FPController player;

    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool isTyping;
    
    [HideInInspector] public bool isDialogueActive;

    private enum ObjectiveState { Correct, Incorrect, InProgress, NotActive }
    private ObjectiveState objectiveState = ObjectiveState.NotActive;

    void Start()
    {
        dialogueUI = DialogueController.Instance;
        player = FindAnyObjectByType<FPController>();
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
        SyncObjectiveState();

        if (objectiveState == ObjectiveState.NotActive)
        {
            dialogueIndex = 0;
        }
        else if (objectiveState == ObjectiveState.InProgress)
        {
            dialogueIndex = dialogueData.inProgressIndex;
        }
        else if (objectiveState == ObjectiveState.Incorrect)
        {
            dialogueIndex = dialogueData.incorrectItemIndex;

            flames.Play();
            SoundManager.Play("Wrong");
        }
        else if (objectiveState == ObjectiveState.Correct)
        {
            dialogueIndex = dialogueData.correctItemIndex;

            stars.Play();
            SoundManager.Play("Correct");
        }

        isDialogueActive = true;

        dialogueUI.SetNPCInfo(dialogueData.npcName);
        dialogueUI.ShowDialogueUI(true);

        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    private void SyncObjectiveState()
    {
        if (dialogueData.objective == null) return;

        string objectiveID = dialogueData.objective.objectiveID;

        if (ObjectiveController.Instance.IsObjCompleted(objectiveID) || ObjectiveController.Instance.isObjHandedIn(objectiveID))
        {
            objectiveState = ObjectiveState.Correct;
        }
        else if (ObjectiveController.Instance.IsObjActive(objectiveID) && player.heldObject == null)
        {
            objectiveState = ObjectiveState.InProgress;
        }
        else if (!ObjectiveController.Instance.IsObjCompleted(objectiveID) && ObjectiveController.Instance.IsObjActive(objectiveID) && player.heldObject != null)
        {
            objectiveState = ObjectiveState.Incorrect;
        }
        else
        {
            objectiveState = ObjectiveState.NotActive;
        }

        // if (compare != null)
        // {
        //     if (compare.IsComparing() == 0)
        //     {
        //         objectiveState = ObjectiveState.Correct;
        //     }
        //     else if (compare.IsComparing() == 1)
        //     {
        //         objectiveState = ObjectiveState.Incorrect;
        //     }
        //     else if (compare.IsComparing() == 2)
        //     {
        //         objectiveState = ObjectiveState.NotActive;
        //     } 
        // }
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();

            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        if (dialogueData.givesObjective[dialogueIndex] && dialogueData.objective != null)
        {
            EndDialogue();
            StartObjective();

            return;
        }

        if (dialogueData.givesItem[dialogueIndex] && dialogueData.objective != null)
        {
            EndDialogue();
            GiveReward();

            return;
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

            SoundManager.PlayVoice("Dialogue");

            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);

            NextLine();
        }
    }

    void StartObjective()
    {
        Debug.Log("Objective Started");

        ObjectiveController.Instance.AcceptObjective(dialogueData.objective);
        objectiveState = ObjectiveState.InProgress;
    }

    void GiveReward()
    {
        Item reward = Instantiate(dialogueData.objective.objectiveReward).GetComponent<Item>();
        reward.PickUp(holdPoint);

        Debug.Log("Reward granted");
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(Typewriter());
    }

    public void EndDialogue()
    {
        if (objectiveState == ObjectiveState.Correct && !ObjectiveController.Instance.isObjHandedIn(dialogueData.objective.objectiveID))
        {
            HandleObjectiveCompletion(dialogueData.objective);
        }

        StopAllCoroutines();

        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);

        PauseController.SetPause(false);
    }

    void HandleObjectiveCompletion(Objective objective)
    {
        ObjectiveController.Instance.HandInObjective(objective.objectiveID);
    }
}
