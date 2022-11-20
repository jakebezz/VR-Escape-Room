using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Static variables to be accessed in various classes
    public static bool alertedGuard;

    [SerializeField] private Player player;

    //Public bool used in Power Off Event
    private bool moveToPowerSwitch;

    //Animation
    private Animator animator;
    private string walk = "Walk";
    private string lookAround = "LookAround";

    private NavMeshAgent agent;

    /// <summary>
    /// Move points for the Guard
    /// movePoint[0] = pointA
    /// movePoint[1] = pointB
    /// movePoint[2] = pointWindow
    /// movePoint[3] = pointPower
    /// </summary>
    [SerializeField] private Transform[] movePoint;

    //Will either be movePoint[0 or 1]
    private Transform patrolPoint;
    private Transform moveLocation;

    //Sets delay before guard moves to location
    [SerializeField] private float waitTime = 10f;

    [SerializeField] private float mainPointWait;
    [SerializeField] private float alertPointWait;

    bool runTimer = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        alertedGuard = false;
        
        moveToPowerSwitch = false;

        transform.position = movePoint[0].position;

        moveLocation = movePoint[1];
        patrolPoint = movePoint[1];
    }

    private void Update()
    {
        if (runTimer)
        {
            if (waitTime > 0)
            {
                Debug.Log("Move Timer " + waitTime);
                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTime = 0;
                agent.SetDestination(moveLocation.position);
                runTimer = false;
            }
        }
        else
        {
            //If player at desination, set next destination as target point, also check if bools are true
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        moveLocation = CheckStates();
                        runTimer = true;
                    }
                }
                else
                {
                    if (alertedGuard == true || moveToPowerSwitch == true)
                    {
                        moveLocation = CheckStates();
                        runTimer = true;
                    }
                }
            }
            else
            {
                if (alertedGuard == true || moveToPowerSwitch == true)
                {
                    moveLocation = CheckStates();
                    runTimer = true;
                }
            }
        }
    }

    private Transform CheckStates()
    {
        if (alertedGuard == false && moveToPowerSwitch == false)
        {
            if (patrolPoint == movePoint[0])
            {
                patrolPoint = movePoint[1];
                waitTime = 10f;
                return patrolPoint;
            }

            if (patrolPoint == movePoint[1])
            {
                patrolPoint = movePoint[0];
                waitTime = 10f;
                return patrolPoint;
            }
        }

        else
        {
            if (alertedGuard == true)
            {
                alertedGuard = false;
                waitTime = 1f;
                return movePoint[2];
            }

            if (moveToPowerSwitch == true)
            {
                moveToPowerSwitch = false;
                waitTime = 1f;
                return movePoint[3];
            }
        }

        waitTime = 10f;
        return patrolPoint;
    }

    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
