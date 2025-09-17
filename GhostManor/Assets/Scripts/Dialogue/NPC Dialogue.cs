using UnityEngine;

//Add NPC and Dialogue System to your Game - Top Down Unity 2D #19
//Game Code Library
//23 Feb 2025
//Code Version: unknown
//Available at: https://youtu.be/eSH9mzcMRqw?si=E06fdp9MJzv0uBRN

[CreateAssetMenu(fileName = "NewNPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string charName;
    public string[] dialogue; //actaul words
    public bool[] endDialogueLines; // where dialogue ends so it doesnt progress
    public Color textColour;
    public AudioClip TTS;
    

    public DialogueState[] actionChoices;

    public int questInProgressIndex; //what they say after giving the quest
    public int questCompletedIndex; // what they say after completing quest
    public Quests quests; //The quest they give

}

[System.Serializable]
public class DialogueState //"options"
{
    public int dialogueIndex;
    //No responses but rather depends on actions
    public int[] nextDialogueIndexes;
    public bool[] givesQuest;
}

