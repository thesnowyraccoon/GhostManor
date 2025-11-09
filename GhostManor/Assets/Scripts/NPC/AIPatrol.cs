using UnityEngine;
using UnityEngine.AI;

//Week 12- Practical Lecture Slides
//Hayes,A. et al.
//Date: 06 November 2025
//Code Version: Unknown 
//Available in Wits DIGA2001A Slides

public class AIPatrol : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    public NPC dialogue;

    [SerializeField]
    private Animator idle;

    private bool isMoving = true;

    void Start()
    {
        idle = GetComponentInParent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (dialogue.isDialogueActive)
        {
            agent.isStopped = true;
        
        }
        else 
        {
            agent.isStopped = false;
            
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentPoint++;

                if (currentPoint >= patrolPoints.Length)
                {
                currentPoint = 0;
                }

            agent.SetDestination(patrolPoints[currentPoint].position);
            }
            
        }


    }
}
