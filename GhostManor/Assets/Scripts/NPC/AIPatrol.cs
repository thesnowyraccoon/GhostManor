using UnityEngine;
using UnityEngine.AI;

//Week 12- Practical Lecture Slides
//Hayes,A. et al.
//Date: 06 November 2025
//Code Version: Unknown 
//Available in Wits DIGA2001A Slides

public class AIPatrol : MonoBehaviour
{
    private NavMeshAgent NPC;
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    [SerializeField]
    private Animator idle;

    private bool isMoving = true;

    void Start()
    {
        idle = GetComponentInParent<Animator>();
        NPC = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        // if dialogueisactive
        // {
        // NPC.isStopped = true;
        //idle.SetBool("isMoving", isMoving); 
        // }

        // else
        if (!NPC.pathPending && NPC.remainingDistance < 0.5f)
        {
            currentPoint++;

            //Reset to the first point
            if (currentPoint >= patrolPoints.Length)
            {
                //NPC.isStopped = false;
                idle.SetBool("isMoving", isMoving);
                currentPoint = 0;
            }

            NPC.SetDestination(patrolPoints[currentPoint].position);
        }


    }
}
