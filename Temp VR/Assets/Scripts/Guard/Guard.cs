using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public static bool alertedGuard = false;                                                    //Static variables to be accessed in various classes

    private string windowTag = "WindowTrigger";                                                 //Tag for the WindowTrigger
    private bool caughtPlayer = false;

    #region References
    [Header("References")]
    [SerializeField] private ColourChangingPuzzle puzzle;                                       //Colour Puzzle
    [SerializeField] private Player player;                                                     //Player
    private NavMeshAgent agent;                                                                 //Guard navmesh agent
    private AudioSource audioSource;                                                            //Audio Source
    [Space(10)]
    #endregion

    #region Movement Locations
    /// <summary>
    /// Move points for the Guard
    /// movePoint[0] = pointA
    /// movePoint[1] = pointB
    /// movePoint[2] = pointWindow
    /// movePoint[3] = pointPower
    /// </summary>
    [SerializeField] private Transform[] movePoint;
    private Transform patrolPoint;                                                                //Two points the Guard Patrols, will either be movePoint[0 or 1]
    private Transform moveToLocation;                                                             //The location guard will move to
    #endregion

    #region Wait Times
    [Header("Wait Times")]
    [Range(5, 10)]
    [SerializeField] private float windowWaitTime;
    [Range(30, 60)]
    [SerializeField] private float powerWaitTime;
    [Range(0, 5)]
    [SerializeField] private float reactionTime;
    [Range(30, 120)]
    [SerializeField] private float patrolPointTime;

    //Sets delay before guard moves to location
    [SerializeField] private float waitTime;
    private bool runTimer = false;
    [Space(10)]
    #endregion

    #region Movement Bools
    //Public bool used in Power Off Event
    private bool moveToPowerSwitch = false;
    private bool atWindow = false;
    private bool atPower = false;
    #endregion

    #region Sound
    [Header("Sound")]
    [SerializeField] private AudioClip[] grunt;
    private bool playCaughtSound = false;
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        transform.position = movePoint[0].position;                                             //Moves the guard to the first point
        moveToLocation = movePoint[1];                                                          //Sets move location to Point B        
        patrolPoint = movePoint[0];                                                             //Sets patrol point to Point A, this will switch to Point B in the CheckStates function    
    }

    /// <summary>
    /// Runs the Guard Movement while runTimer is true
    /// </summary>
    private void Update()
    {
        if (runTimer)
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;

                audioSource.Stop();                                                             //Stops audio when guard is still

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
                audioSource.Play();                                                             //Plays audio when they walk

                waitTime = 0;
                agent.SetDestination(moveToLocation.position);
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

    #region Patrol Points
    /// <summary>
    /// Repeat Patrol points
    /// </summary>
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
        moveToLocation = patrolPoint;
    }

    /// <summary>
    /// Moves guard to Window Point
    /// </summary>
    private void WindowPoint()
    {
        SoundManager.Instance.PlaySoundAtPoint(grunt[UnityEngine.Random.Range(0, 3)], transform.position, 0.7f);

        waitTime = reactionTime;
        moveToLocation = movePoint[2];
        atWindow = true;
        alertedGuard = false;
        runTimer = true;
    }

    /// <summary>
    /// Moves guard to Power Point
    /// </summary>
    private void PowerPoint()
    {
        SoundManager.Instance.PlaySoundAtPoint(grunt[UnityEngine.Random.Range(0, 3)], transform.position, 0.7f);

        waitTime = reactionTime;
        moveToLocation = movePoint[3];
        atPower = true;
        moveToPowerSwitch = false;
    }
    #endregion

    #region Destination Logic
    /// <summary>
    /// Selects the next destination for the guard while they wait
    /// </summary>
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

    /// <summary>
    /// Checks if guard is alerted while they are moving
    /// </summary>
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

    /// <summary>
    /// Turns on power after powerWaitTime duration
    /// </summary>
    /// <returns></returns>
    public IEnumerator TurnOnPower()
    {
        waitTime = powerWaitTime;
        atPower = false;
        yield return new WaitForSeconds(powerWaitTime);
        puzzle.powerOnEvent.Invoke();
    }
    #endregion

    /// <summary>
    /// Checks if player is hidden when guard is near the window
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(windowTag))
        {
            if (player.isHidden == false && caughtPlayer == false)
            {
                caughtPlayer = true;
                agent.isStopped = true;

                GameManager.Instance.CaughtPlayer();
                SoundManager.Instance.PlaySoundAtPoint(grunt[UnityEngine.Random.Range(0, 3)], transform.position, 0.7f);
            }
        }
    }

    /// <summary>
    /// Sets moveToPowerSwitch to true - Called in Power Off Event to move guard to powerPoint
    /// </summary>
    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
