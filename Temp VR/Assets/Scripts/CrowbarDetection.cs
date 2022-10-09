using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarDetection : MonoBehaviour
{
    private Rigidbody crowbar;
    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    [SerializeField] private float crowbarVelocity;

    //Used to duplicate the location of the crowbar, stops the guard from folling the crowbar object
    [SerializeField] private GameObject crowbarLocation;

    //Access to Guard Class
    private Guard guard;
    [SerializeField] private GameObject guardObj;

    void Start()
    {
        crowbar = GetComponent<Rigidbody>();
        crowbar.isKinematic = true;

        guard = guardObj.GetComponent<Guard>();
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

            //Clones the crowbar location
            GameObject crowbarLocationClone;
            crowbarLocationClone = Instantiate(crowbarLocation, crowbarLocation.transform.position, crowbarLocation.transform.rotation);

            //Sets the global variables 
            guard.alertedGuards = true;
            guard.alertedLocation = crowbarLocationClone.transform;
        }
    }
}
