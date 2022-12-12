using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class Crowbar : MonoBehaviour
{
    private Rigidbody crowbar;
    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    [SerializeField] private float velocity;

    [SerializeField] private Grabbable grabbableLid;

    [SerializeField] private GameObject crowbarLidPlacement;
    private bool inLidTrigger;

    [SerializeField] MicrophoneDetection micDetection;

    //Tag
    private string floorTag = "Floor";
    private string crateLidTag = "CrateLidTrigger";

    void Start()
    {
        crowbar = GetComponent<Rigidbody>();
        crowbar.isKinematic = true;

        grabbableLid.enabled = false;
        crowbarLidPlacement.SetActive(false);
    }

    void Update()
    {
        //sets the velocity 
        velocity = crowbar.velocity.magnitude;

        //Will change from space to when an object is in a trigger box near or just remove kinematic completely and balance it on somehting
        if(Input.GetKeyDown(KeyCode.Space))
        {
            crowbar.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(floorTag) && velocity > 2f)
        {
            //Delete this
            Debug.Log("Guard Alerted");

            //Sets the global variables 
            Guard.alertedGuard = true;
        }

        if (other.CompareTag(crateLidTag))
        {
            inLidTrigger = true;
            crowbarLidPlacement.SetActive(true);
        }

        //If player throws an object that has a rigidbody
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            crowbar.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(crateLidTag))
        {
            inLidTrigger = false;
            crowbarLidPlacement.SetActive(false);
        }
    }

    public void MoveToHighlight()
    {
        if (inLidTrigger == true)
        {
            gameObject.transform.position = crowbarLidPlacement.transform.position;
            gameObject.transform.rotation = crowbarLidPlacement.transform.rotation;
            crowbar.isKinematic = true;
            crowbarLidPlacement.SetActive(false);
            grabbableLid.enabled = true;
        }
    }

    public void LeaveHighlight()
    {
        if (inLidTrigger == true)
        {
            inLidTrigger = false;
            crowbar.isKinematic = false;
            crowbarLidPlacement.SetActive(false);
        }
    }
}
