using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    #region RayCast
    [SerializeField] private float hitRange;                            //Range that Raycast can hit
    private RaycastHit hit;
    #endregion

    [SerializeField] private float force;                               //Force to be added to Dynamite    
    [Header("References")]
    [SerializeField] private GameObject sprayPoint;                     //Spray Point object
    [SerializeField] private GameObject highlightVent;                  //Vent Highlight
    [SerializeField] private ParticleSystem smoke;                      //Smoke particle effect
    [SerializeField] private Dynamite dynamite;                         //Dynamite script

    private Rigidbody dynamiteRigid;                                    //Dynamite rigidbody

    private void Start()
    {
        dynamiteRigid = dynamite.bombPartRigid;
    }

    private void Update()
    {
        //Raycast from FireExtinguisher
        if (Physics.Raycast(sprayPoint.transform.position, sprayPoint.transform.forward, out hit, hitRange))
        {
            //If raycast hits the vent and the dynamite is in vent add force to it
            if (hit.collider.tag == "Vent" && dynamite.outOfVent == false)
            {
                highlightVent.SetActive(true);
                dynamiteRigid.AddForce(-Vector3.forward * force);

                if (!smoke.isPlaying)
                {
                    smoke.Play();
                }
            }

            //If the hit collider has a rigidbody you can add force to it
            else if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(transform.forward * force);

                if (!smoke.isPlaying)
                {
                    smoke.Play();
                }
            }

            else
            {
                highlightVent.SetActive(false);
                smoke.Stop();
            }
        }
    }
}
