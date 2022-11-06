using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Static variables to be accessed in various classes
    public static bool canSeePlayer;
    public static bool alertedGuards;
    public static Vector3 alertedLocation;

    //Animation
    private Animator animator;
    private string walk = "Walk";
    private string lookAround = "LookAround";

    private NavMeshAgent guardAgent;

    //Guard original location that they will move back to
    [SerializeField] private Transform guardStartLocation;

    //Sets delay before guard moves to location
    [SerializeField] private int waitToMove;
    //Different vaule for return so that guard stays around sound location a bit longer
    [SerializeField] private int waitToReturn;

    private void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        alertedGuards = false;
        canSeePlayer = false;
    }

    void Update()
    {
        //Moves the guard to the location of the sound
        if (alertedGuards == true)
        {
            StartCoroutine(WaitToMoveToAlert(alertedLocation, waitToMove));
        }

        //If the guard is near the alerted location and standing still he will look around and then return to his post a few seconds later
        if (Vector3.Distance(alertedLocation, transform.position) <= 1.0f && guardAgent.velocity.magnitude == 0f)
        {
            Debug.Log("Guard At Destination");
            animator.Play(lookAround, 0, 0.0f);
            alertedGuards = false;
            StartCoroutine(WaitToMoveToAlert(guardStartLocation.position, waitToReturn));
        }
    }

    //Delays the movement of the guard so they dont move instanly
    IEnumerator WaitToMoveToAlert(Vector3 location, float wait)
    {
        yield return new WaitForSeconds(wait);
        guardAgent.destination = location;
        animator.Play(walk, 0, 0.0f);
    }
}
