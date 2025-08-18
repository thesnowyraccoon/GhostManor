using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public string charName;
    public string[] dialogue; //actaul words
    public Color textColour;
    public int index; //to track what line is what

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
