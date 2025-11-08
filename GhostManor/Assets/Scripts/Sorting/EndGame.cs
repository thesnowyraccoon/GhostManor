using Unity.Physics;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject[] Pillars;
    public GameObject EndScreen;

    void Start()
    {
        EndScreen.SetActive(false);
    }
    void EndGameState()
    {
        // if (All the pillars have items)
        // {
        //     EndScreen.SetActve(true);   
        // }
    }
    
   
}
