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

    [SerializeField] private AudioClip hitFloorSound;
    [SerializeField] private AudioClip hitPillowSound;

    //Tag
    private string floorTag = "Floor";
    private string pillowTag = "Pillow";
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
            SoundManager.Instance.PlaySoundAtPoint(hitFloorSound, transform.position, 0.01f);

            //Sets the global variables 
            Guard.alertedGuard = true;
        }
        else if (other.CompareTag(pillowTag) && velocity > 2f)
        {
            SoundManager.Instance.PlaySoundAtPoint(hitPillowSound, transform.position, 0.01f);
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
