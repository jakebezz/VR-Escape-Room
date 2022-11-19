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

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        alertedGuard = false;
        
        moveToPowerSwitch = false;

        transform.position = movePoint[0].position;

        targetPoint = movePoint[1];
        StartCoroutine(WaitToMoveToPoint(movePoint[1], waitTime));
    }


    //Delays the movement of the guard so they dont move instanly
    private IEnumerator WaitToMoveToPoint(Transform moveLoc, float wait)
    {
        while (true)
        {
            if (alertedGuard == true)
            {
                moveLoc = movePoint[2];
            }

            else if(moveToPowerSwitch == true)
            {
                moveLoc = movePoint[3];
            }

            else
            {
                moveLoc = targetPoint;
            }

            yield return new WaitForSeconds(wait);
            agent.SetDestination(moveLoc.position);
            //animator.Play(walk, 0, 0.0f);

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (moveLoc == movePoint[0])
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
                        moveLoc = targetPoint;
                    }

                    if(moveLoc == movePoint[3])
                    {
                        moveToPowerSwitch = false;
                        moveLoc = targetPoint;
                    }

                    Debug.Log("Point has been reached");
                }
            }
        }
    }

    public void PowerSwitchOn()
    {
        moveToPowerSwitch = true;
    }
}
