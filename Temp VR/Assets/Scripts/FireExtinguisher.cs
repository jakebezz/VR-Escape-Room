using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    //ADD BOOL SO FORCE ISNT ADDED ONCE DYNAMITE IS OUT OF VENT
    [SerializeField] private GameObject dynamiteObj;
    private Rigidbody dynamiteRigid;
    private bool dynamiteOutOfVent;

    [SerializeField] private float force;

    //Range that Raycast can hit
    [SerializeField] private float hitRange;
    private RaycastHit hit;

    private void Start()
    {
        dynamiteRigid = dynamiteObj.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Vent" && dynamiteOutOfVent == false)
            {
                Debug.Log("Hit Collider");
                if (Input.GetKey(KeyCode.Y))
                {
                    dynamiteRigid.AddForce(-Vector3.forward * force);
                }
            }

            //If the hit object has a rigidbody the player is able to add force
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
