using UnityEngine;
using TMPro;
using System.Collections;

// 5 Minute DIALOGUE SYSTEM in UNITY Tutorial
// BMo
// 19 March 2021
// Code Version: Unknown
// Available at https://youtu.be/8oTYabhj248?si=Y4JJXjdaxYVkIulq

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI newText;
    public string[] dialogue;
    public float textSpeed;
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //when you click left mouse button it will fill in lines
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
}
