using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private Animator animator;
    private string walk = "Walk";
    private string lookAround = "LookAround";

    //Guard original location that they will move back to
    [SerializeField] private Transform guardStartLocation;
    private NavMeshAgent guardAgent;

    //Sets delay before guard moves to location
    [SerializeField] private int waitToMove;
    //Different vaule for return so that guard stays around sound location a bit longer
    [SerializeField] private int waitToReturn;

    private void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Moves the guard to the location of the sound
        if (GameManager.Instance.alertedGuards == true)
        {
            StartCoroutine(WaitToMoveToAlert(GameManager.Instance.alertedLocation, waitToMove));
        }

        if (Vector3.Distance(GameManager.Instance.alertedLocation, transform.position) <= 1.0f && guardAgent.velocity.magnitude == 0f)
        {
            Debug.Log("Guard At Destination");
            animator.Play(lookAround, 0, 0.0f);
            GameManager.Instance.alertedGuards = false;
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
