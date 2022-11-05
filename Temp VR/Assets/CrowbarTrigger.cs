using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody crowbar;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Ball")
        {
            crowbar.isKinematic = false;
        }
    }
}
