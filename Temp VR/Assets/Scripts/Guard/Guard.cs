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
    private Transform targetPoint;

    //Sets delay before guard moves to location
    [SerializeField] private int waitTime;

    //Bool to loop the couroutine
    private bool moving;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        alertedGuard = false;
        canSeePlayer = false;
        
        moveToPowerSwitch = false;

        transform.position = movePoint[0].position;
        targetPoint = movePoint[1];
    }

    private void Update()
    {
        if(alertedGuard == false && moveToPowerSwitch == false)
        {
            MoveToTarget();
        }
        else if(alertedGuard == true)
        {
            MoveToAlert(movePoint[2]);
        }
        else if(moveToPowerSwitch == true)
        {
            MoveToAlert(movePoint[3]);
        }
    }


    //Delays the movement of the guard so they dont move instanly
    private IEnumerator WaitToMoveToPoint(Transform moveLoc, float wait)
    {
        moving = true;
        while (moving)
        {
            yield return new WaitForSeconds(wait);
            agent.SetDestination(moveLoc.position);
            //animator.Play(walk, 0, 0.0f);

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if(moveLoc == movePoint[0])
                    {
                        targetPoint = movePoint[1];
                    }

                    if(moveLoc == movePoint[1])
                    {
                        targetPoint = movePoint[0];
                    }

                    if(moveLoc == movePoint[2])
                    {
                        alertedGuard = false;

                        if(player.isHidden == false)
                        {
                            Debug.Log("Player Spotted");
                        }
                    }

                    if(moveLoc == movePoint[3])
                    {
                        moveToPowerSwitch = false;
                    }

                    moving = false;
                    Debug.Log("Point has been reached");
                }
            }
        }
    }

    private void MoveToAlert(Transform point)
    {
        StartCoroutine(WaitToMoveToPoint(point, 1));
        StartCoroutine(WaitToMoveToPoint(targetPoint, 5));
    }

    private void MoveToTarget()
    {
        StartCoroutine(WaitToMoveToPoint(targetPoint, waitTime));
    }

    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
