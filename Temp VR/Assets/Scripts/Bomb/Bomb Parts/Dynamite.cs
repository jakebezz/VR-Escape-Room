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
    private string ventCollision = "VentCollision";
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

            if (outOfVent == false)
            {
                outOfVent = true;
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
            if (grabbable.enabled == false)
            {
                grabbable.enabled = true;
            }

            if (outOfVent == false)
            {
                outOfVent = true;
            }

            if (velocity > 2f)
            {
                SoundManager.Instance.PlaySoundAtPoint(hitGroundSound, transform.position, 0.01f);
            }
        }

        //Dynamite hits back of vent
        if (collision.gameObject.CompareTag(ventCollision))
        {
            if (outOfVent == false)
            {
                outOfVent = true;
            }
        }
    }
}
