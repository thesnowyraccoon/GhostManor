using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [SerializeField] Object objectType;
    [SerializeField] NPC npc;
    private string sort;

    enum Object { Red, Blue, Green } //Object types
    enum NPC { Red, Blue, Green } //NPC categories

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC")) //only applies to NPCs
        {
            //sort();
        }
    }

    // sort(object)
    // {
    //     If Object.Red = NPC.Red
    //     {
    //         //delete the game object
    //     }
    //     else
    //     {
    //         Debug.Log("Wrong Item");
    //     }
    // }

}
