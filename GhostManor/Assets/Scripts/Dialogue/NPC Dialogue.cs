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
    public Color textColour;
}
