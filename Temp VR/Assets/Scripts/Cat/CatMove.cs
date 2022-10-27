using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{
    public NavMeshAgent catAgent;

    private void Start()
    {
        catAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerTrigger")
        {
            Debug.Log("Power Off");
        }
    }
}
