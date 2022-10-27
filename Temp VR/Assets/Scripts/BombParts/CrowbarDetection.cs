using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarDetection : MonoBehaviour
{
    private Rigidbody crowbar;
    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    [SerializeField] private float crowbarVelocity;

    //Used to duplicate the location of the crowbar, stops the guard from folling the crowbar object
    [SerializeField] public Vector3 crowbarLocation;

    void Start()
    {
        crowbar = GetComponent<Rigidbody>();
        crowbar.isKinematic = true;
    }

    void Update()
    {
        //sets the velocity 
        crowbarVelocity = crowbar.velocity.magnitude;

        //Will change from space to when an object is in a trigger box near or just remove kinematic completely and balance it on somehting
        if(Input.GetKeyDown(KeyCode.Space))
        {
            crowbar.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor" && crowbarVelocity > 2)
        {
            //Delete this
            Debug.Log("Guard Alerted");

            crowbarLocation = crowbar.transform.position;

            //Sets the global variables 
            GameManager.Instance.alertedGuards = true;
            GameManager.Instance.alertedLocation = crowbarLocation;
        }
    }
}
