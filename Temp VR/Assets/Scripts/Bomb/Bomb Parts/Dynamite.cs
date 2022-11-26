using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : BombParts
{
    //Bool - used so that the fire extinguisher doesnt keep affecting the dynamite when its not in the vent
    public bool outOfVent;
    //Object velocity
    [SerializeField] private float velocity;

    //Tags
    private string pillowTag = "Pillow";
    private string floorTag = "Floor";

    protected override void Start()
    {
        outOfVent = false;
        base.Start();
    }

    private void Update()
    {
        //Sets velocity to the objects velocity
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
