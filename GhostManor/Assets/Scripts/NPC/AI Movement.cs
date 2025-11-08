using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent NPC;
    
    void Start()
    {
        NPC = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            NPC.SetDestination(target.position); // move towards target
        }
    }
}
