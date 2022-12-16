using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Oculus.Interaction;

public class Dynamite : BombParts
{
    [Space(20)]
    [SerializeField] private AudioClip hitGroundSound;                  //Sound when hitting ground
    [NonSerialized] public bool outOfVent;                              //Bool - used so that the fire extinguisher doesnt keep affecting the dynamite when its not in the vent
    private Grabbable grabbable;                                        //Reference to Oculus Grabbable
    private float velocity;                                             //Object velocity

    #region Tags
    private string pillowTag = "Pillow";
    private string floorTag = "Floor";
    private string dynamiteTrigger = "DynamiteTrigger";
    #endregion

    protected override void Start()
    {
        base.Start();

        outOfVent = false;

        grabbable = GetComponent<Grabbable>();
        grabbable.enabled = false;
    }

    private void Update()
    {
        velocity = bombPartRigid.velocity.magnitude;                   //Sets velocity to the objects velocity
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Dynamite hitting floor
        if (collision.gameObject.CompareTag(floorTag))
        {
            if (grabbable.enabled == false)
            {
                grabbable.enabled = true;
            }

            //Alert guard and play loud sound if Velocity is too high when hitting floor
            if (velocity > 2f)
            {
                SoundManager.Instance.PlaySoundAtPoint(hitGroundSound, transform.position, 0.1f);

                Guard.alertedGuard = true;
            }
        }

        //Dynamite hitting pillow
        if (collision.gameObject.CompareTag(pillowTag))
        {
            grabbable.enabled = true;

            SoundManager.Instance.PlaySoundAtPoint(hitGroundSound, transform.position, 0.01f);
        }
    }

    //Set outOfVent true when Dynamite has reached the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == dynamiteTrigger && outOfVent == false)
        {
            outOfVent = true;
        }
    }
}
