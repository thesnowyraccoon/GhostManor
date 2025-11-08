using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{
    private NavMeshAgent NPC;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    void Start()
    {
        NPC = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (!NPC.pathPending&& NPC.remainingDistance < 0.5f)
        {
            currentPoint++;

            //Reset to the first point
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }

            NPC.SetDestination(patrolPoints[currentPoint].position);
        }
    }
}
