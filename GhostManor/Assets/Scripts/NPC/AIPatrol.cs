using UnityEngine;
using UnityEngine.AI;

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
        // NPC.acceleration = 0f;
        //idle.SetBool("isMoving", isMoving); 
        // }

        // else
        if (!NPC.pathPending && NPC.remainingDistance < 0.5f)
        {
            currentPoint++;

            //Reset to the first point
            if (currentPoint >= patrolPoints.Length)
            {
                //NPC.acceleration = 120f;
                idle.SetBool("isMoving", isMoving); 
                currentPoint = 0;
            }

            NPC.SetDestination(patrolPoints[currentPoint].position);
        }

        
    }
}
