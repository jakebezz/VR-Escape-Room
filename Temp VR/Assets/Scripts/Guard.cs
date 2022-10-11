using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Guard original location that they will move back to
    [SerializeField] private Transform guardStartLocation;
    [SerializeField] private NavMeshAgent guardAgent;

    //For testing DELETE THIS
    [SerializeField] int waitTime;

    private void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Moves the guard to the location of the sound
        if (GameManager.Instance.alertedGuards == true)
        {
            StartCoroutine(WaitToMoveToAlert());
        }
    }

    //Delays the movement of the guard so they dont move instanly
    IEnumerator WaitToMoveToAlert()
    {
        yield return new WaitForSeconds(waitTime);
        guardAgent.destination = GameManager.Instance.alertedLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DetectableObject")
        {
            if(GameManager.Instance.canSeePlayer == false)
            {
                GameManager.Instance.alertedGuards = false;
                GameManager.Instance.alertedLocation = guardStartLocation.transform.position;
                StartCoroutine(WaitToMoveToAlert());
            }
        }
    }
}
