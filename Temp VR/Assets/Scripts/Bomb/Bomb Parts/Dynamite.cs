using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : BombParts
{
    public bool outOfVent;
    [SerializeField] private float velocity;

    private string pillowTag = "Pillow";
    private string floorTag = "Floor";

    private void Start()
    {
        outOfVent = false;
        bombPartRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity = bombPartRigid.velocity.magnitude;
    }

    //If the dyanamite is out of the vent and/or hits the ground too fast
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(pillowTag))
        {
            outOfVent = true;
        }

        if (collision.gameObject.CompareTag(floorTag) && velocity > 2f)
        {
            outOfVent = true;

            //Delete this
            Debug.Log("Guard Alerted");

            //Sets the global variables 
            Guard.alertedGuard = true;
        }
    }
}
