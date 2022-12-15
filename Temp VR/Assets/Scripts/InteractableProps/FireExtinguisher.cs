using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Dynamite dynamite;
    private Rigidbody dynamiteRigid;

    [SerializeField] private float force;

    [SerializeField] private GameObject sprayPoint;

    [SerializeField] private GameObject highlightVent;

    //Range that Raycast can hit
    [SerializeField] private float hitRange;
    private RaycastHit hit;

    [SerializeField] private ParticleSystem smoke;

    private void Start()
    {
        dynamiteRigid = dynamite.bombPartRigid;
    }

    //MOVE FROM UPDATE INTO WHEN BUTTON IS PRESSED OR HANDLE IS SQUEEZED
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
