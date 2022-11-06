using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Dynamite dynamite;
    private Rigidbody dynamiteRigid;

    [SerializeField] private float force;

    [SerializeField] private GameObject sprayPoint;

    //Range that Raycast can hit
    [SerializeField] private float hitRange;

    private void Start()
    {
        dynamiteRigid = dynamite.bombPartRigid;
    }

    private void Update()
    {
        //Raycast from FireExtinguisher
        if (Physics.Raycast(sprayPoint.transform.position, sprayPoint.transform.forward, out RaycastHit hit, hitRange))
        {
            //If raycast hits the vent and the dynamite is in vent add force to it
            if (hit.collider.tag == "Vent" && dynamite.outOfVent == false)
            {
                Debug.Log("Hit Vent");
                if (Input.GetKey(KeyCode.Y))
                {
                    dynamiteRigid.AddForce(-Vector3.forward * force);
                }

                //If the hit collider has a rigidbody you can add force to it
                if (hit.rigidbody != null)
                {
                    Debug.Log("Hit Rigidbody");
                    if (Input.GetKey(KeyCode.Y))
                    {
                        hit.rigidbody.AddForce(transform.forward * force);
                    }
                }
            }
        }
    }
}
