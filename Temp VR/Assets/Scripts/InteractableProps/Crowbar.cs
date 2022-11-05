using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour
{
    private Rigidbody crowbar;
    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    [SerializeField] private float velocity;

    void Start()
    {
        crowbar = GetComponent<Rigidbody>();
        crowbar.isKinematic = true;
    }

    void Update()
    {
        //sets the velocity 
        velocity = crowbar.velocity.magnitude;

        //Will change from space to when an object is in a trigger box near or just remove kinematic completely and balance it on somehting
        if(Input.GetKeyDown(KeyCode.Space))
        {
            crowbar.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor" && velocity > 2f)
        {
            //Delete this
            Debug.Log("Guard Alerted");

            //Sets the global variables 
            GameManager.Instance.alertedGuards = true;
            GameManager.Instance.alertedLocation = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            crowbar.isKinematic = false;
        }
    }
}
