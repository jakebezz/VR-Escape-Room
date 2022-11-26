using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Static variables to be accessed in various classes
    public static bool alertedGuard = false;

    [SerializeField] private ColourChangingPuzzle puzzle;

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

    //Repeat Patrol points
    private void RepeatPatrol()
    {
        //Swaps the patrol points to the two points
        if (patrolPoint == movePoint[0])
        {
            patrolPoint = movePoint[1];
        }
        else
        {
            patrolPoint = movePoint[0];
        }

        //Changes the wait time to the windowWaitTime, in this function becuase the guard will go straight to the patrol point there were heading to after the wait time
        if (atWindow == true)
        {
            waitTime = windowWaitTime;
            atWindow = false;
        }
        //Starts Courotine
        else if (atPower == true)
        {
            StartCoroutine(TurnOnPower());
        }
        //Deafult wait time at the patrol points
        else
        {
            waitTime = patrolPointTime;
        }
        //Sets moveLocation to patrolPoint
        moveLocation = patrolPoint;
    }

    //Moves guard to Window Point
    private void WindowPoint()
    {
        waitTime = reactionTime;
        moveLocation = movePoint[2];
        atWindow = true;
        alertedGuard = false;
        runTimer = true;
    }

    //Moves guard to Power Point
    private void PowerPoint()
    {
        waitTime = reactionTime;
        moveLocation = movePoint[3];
        atPower = true;
        moveToPowerSwitch = false;
    }

    //Selects the next destination for the guard while they wait
    private void AtDestination()
    {
        //If guard is alerted go to window point and start timer
        if (alertedGuard == true)
        {
            WindowPoint();
            runTimer = true;
        }
        //If power is turned off, go to the power point and start timer
        else if (moveToPowerSwitch == true)
        {
            PowerPoint();
            runTimer = true;
        }
        //If nothing happened, resume patrol to deafult points
        else
        {
            RepeatPatrol();
            runTimer = true;
        }
    }
    
    //Checks if guard is alerted while they are moving
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

    //Checks if player is hidden when guard is near the window
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
    //Turns on power after 30 seconds
    public IEnumerator TurnOnPower()
    {
        waitTime = powerWaitTime;
        atPower = false;
        yield return new WaitForSeconds(powerWaitTime);
        puzzle.powerOn.Invoke();
    }
}
