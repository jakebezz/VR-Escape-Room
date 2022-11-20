using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Static variables to be accessed in various classes
    public static bool alertedGuard = false;

    //Guard navmesh agent
    private NavMeshAgent agent;

    //Tests if player is hidden when guard is at window
    [SerializeField] private Player player;

    //Public bool used in Power Off Event
    private bool moveToPowerSwitch = false;

    //Animation
    private Animator animator;
    private string walk = "Walk";
    private string lookAround = "LookAround";


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
    //The location guard will move to
    private Transform moveLocation;

    //Sets delay before guard moves to location
    [SerializeField] private float waitTime = 10f;
    private bool runTimer = false;

    //How long guard will wait at points
    [SerializeField] private float windowWaitTime = 30f;
    [SerializeField] private float powerWaitTime = 30f;
    [SerializeField] private float reactionTime = 5f;
    [SerializeField] private float patrolPointTime = 10f;

    private bool atWindow = false;
    private bool atPower = false;

    private void Start()
    {
        //Get components
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Moves the guard to the first point
        transform.position = movePoint[0].position;

        //Sets move location to Point B
        moveLocation = movePoint[1];
        //Sets patrol point to Point A, this will switch to Point B in the CheckStates function
        patrolPoint = movePoint[0];
    }

    //Wanted this to be a Coroutine but it wouldn't work :(
    private void Update()
    {
        //If the timer is being run
        if (runTimer)
        {
            if (waitTime > 0)
            {
                //Debug.Log("Move Timer " + waitTime);
                waitTime -= Time.deltaTime;

                if (alertedGuard == true || moveToPowerSwitch == true)
                {
                    waitTime = reactionTime;
                    moveLocation = CheckStates();
                }
            }
            //Moves agent to destination when timer is 0
            else
            {
                waitTime = 0;
                agent.SetDestination(moveLocation.position);
                runTimer = false;
            }
        }
        else
        {
            //If agent has path
            if (!agent.pathPending)
            {
                //If agent is at destination
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        moveLocation = CheckStates();
                        runTimer = true;
                    }
                }
                //If agent is moving to destination
                else
                {
                    if (alertedGuard == true || moveToPowerSwitch == true)
                    {
                        moveLocation = CheckStates();
                        runTimer = true;
                    }
                }
            }
            //If agent doesnt have path
            else
            {
                //If bools are active
                if (alertedGuard == true || moveToPowerSwitch == true)
                {
                    moveLocation = CheckStates();
                    runTimer = true;
                }
                else
                {
                    moveLocation = CheckStates();
                    runTimer = true;
                }
            }
        }
    }

    private Transform CheckStates()
    {
        //If the alert bools are false, guard will patrol up and down street
        if (alertedGuard == false && moveToPowerSwitch == false)
        {
            if (patrolPoint == movePoint[0])
            {
                //Swaps the patrol point
                patrolPoint = movePoint[1];

                //If guard moves to window from partrol point
                if (atWindow == true)
                {
                    waitTime = windowWaitTime;
                    atWindow = false;
                }
                //If guard moves to power from partrol point
                else if (atPower == true)
                {
                    waitTime = powerWaitTime;
                    atPower = false;
                }
                //If guard moved from partrol point to partol point
                else
                {
                    waitTime = patrolPointTime;
                }

                return patrolPoint;
            }

            if (patrolPoint == movePoint[1])
            {
                patrolPoint = movePoint[0];

                if (atWindow == true)
                {
                    waitTime = windowWaitTime;
                    atWindow = false;
                }
                else if (atPower == true)
                {
                    waitTime = powerWaitTime;
                    atPower = false;
                }
                else
                {
                    waitTime = patrolPointTime;
                }

                return patrolPoint;
            } 
        }

        else
        {
            //Alerted guard values
            if (alertedGuard == true)
            {
                atWindow = true;
                alertedGuard = false;
                waitTime = reactionTime;
                return movePoint[2];
            }

            //Power switch value
            if (moveToPowerSwitch == true)
            {
                atPower = true;
                moveToPowerSwitch = false;
                waitTime = reactionTime;
                return movePoint[3];
            }
        }

        //Default return
        waitTime = patrolPointTime;
        return patrolPoint;
    }

    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
