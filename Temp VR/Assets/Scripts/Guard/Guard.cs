using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Guard original location that they will move back to
    [SerializeField] private Transform guardStartLocation;
    private NavMeshAgent guardAgent;

    //Sets delay before guard moves to location
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


}
