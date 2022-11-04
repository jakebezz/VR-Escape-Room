using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Dynamite dynamite;
    private Rigidbody dynamiteRigid;

    [SerializeField] private float force;

    //Range that Raycast can hit
    [SerializeField] private float hitRange;
    private RaycastHit extinguisherHit;

    private void Start()
    {
        dynamiteRigid = dynamite.bombPartRigid;
    }

    private void Update()
    {
        extinguisherHit = GameManager.Instance.RaycastFromObject(gameObject);

        if (extinguisherHit.collider.tag == "Vent" && dynamite.outOfVent == false)
        {
            Debug.Log("Hit Collider");
            if (Input.GetKey(KeyCode.Y))
            {
                dynamiteRigid.AddForce(-Vector3.forward * force);
            }

            if (extinguisherHit.rigidbody != null)
            {
                Debug.Log("Hit Rigidbody");
                if (Input.GetKey(KeyCode.Y))
                {
                    extinguisherHit.rigidbody.AddForce(transform.forward * force);
                }
            }
        }
    }
}
