using Unity.Physics;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Transform Pillars;
    public GameObject EndScreen;

    void Start()
    {
        EndScreen.SetActive(false);
    }
    public void CheckEndGame()
    {
        foreach (Transform child in Pillars)
        {
            if (child.childCount >= 1)
            {
                Debug.Log("ObjcetCheck");
            } 
        }
        // if (All the pillars have items)
        // {
        //     EndScreen.SetActve(true);   
        // }
    }
    
   
}
