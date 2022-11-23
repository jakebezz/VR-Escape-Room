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

    private string windowTag = "WindowTrigger";

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


    //MAYBE MAKE TRIGGERS AT LOCATIONS AND CHANGE UPDATE TO ONTRIGGER ENTER/STAY
    private void Update()
    {
        //If the timer is being run
        if (runTimer)
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;

                if (alertedGuard == true)
                {
                    WindowPoint();
                }

                if (moveToPowerSwitch == true)
                {
                    PowerPoint();
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
                        AtDestination();
                    }
                }
                //If agent is moving to destination
                else
                {
                    MovingToDestination();
                }
            }
        }
    }

    private void RepeatPatrol()
    {
        if (patrolPoint == movePoint[0])
        {
            patrolPoint = movePoint[1];
        }
        else
        {
            patrolPoint = movePoint[0];
        }

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

        moveLocation = patrolPoint;
    }

    private void WindowPoint()
    {
        waitTime = reactionTime;
        moveLocation = movePoint[2];
        atWindow = true;
        alertedGuard = false;
        runTimer = true;
    }

    private void PowerPoint()
    {
        waitTime = reactionTime;
        moveLocation = movePoint[3];
        atPower = true;
        moveToPowerSwitch = false;
    }

    private void AtDestination()
    {
        if (alertedGuard == true)
        {
            WindowPoint();
            runTimer = true;
        }

        else if (moveToPowerSwitch == true)
        {
            PowerPoint();
            runTimer = true;
        }
        else
        {
            RepeatPatrol();
            runTimer = true;
        }
    }
    
    private void MovingToDestination()
    {
        if (alertedGuard == true)
        {
            WindowPoint();
            runTimer = true;
        }

        if (moveToPowerSwitch == true)
        {
            PowerPoint();
            runTimer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(windowTag))
        {
            Debug.Log("Guard in Trigger");
            if (player.isHidden == false)
            {
                Debug.Log("Guard Caught Player");
                WindowPoint();
                //End Game Event
            }
        }
    }

    //Used in Power Off Event
    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
