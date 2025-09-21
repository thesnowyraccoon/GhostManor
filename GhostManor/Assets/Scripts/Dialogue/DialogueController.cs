using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Add NPC and Dialogue System to your Game - Top Down Unity 2D #19
// Game Code Library
// 23 Feb 2025 
// Code Version: Unknown
// Available at: https://youtu.be/eSH9mzcMRqw?si=EnQGNmLLeNjehw7f 

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    public Transform choiceContainer;
    public GameObject choicePrefab;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNPCInfo(string npcName)
    {
        nameText.text = npcName;
        //portraitImage.sprite = portrait;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
