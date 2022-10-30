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
    private RaycastHit extinguisherHit;

    private void Start()
    {
        dynamiteRigid = dynamiteObj.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        extinguisherHit = GameManager.Instance.RaycastFromObject(gameObject);

        if (extinguisherHit.collider.gameObject.tag == "Vent" && dynamiteOutOfVent == false)
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
