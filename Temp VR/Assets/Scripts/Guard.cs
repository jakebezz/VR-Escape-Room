using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    //Guard original location that they will move back to
    [SerializeField] private Transform guardStartLocation;
    [SerializeField] private NavMeshAgent guardAgent;

    //Will be used in mose classes, may change to static
    public bool alertedGuards;
    public Transform alertedLocation;

    private void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        alertedGuards = false;
    }

    void Update()
    {
        //Moves the guard to the location of the sound
        if (alertedGuards == true)
        {
            guardAgent.destination = alertedLocation.position;
        }
    }
}
