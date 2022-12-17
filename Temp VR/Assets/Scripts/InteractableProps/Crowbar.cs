using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class Crowbar : MonoBehaviour
{
    #region Crowbar
    private Rigidbody crowbarRigid;
    [SerializeField] private float velocity;                                                    //Crowbar Velocity, is its own variable to make playtesting adjustments easier
    private Crowbar crowbarScript;
    #endregion
    
    #region Wooden Crate Lid
    [SerializeField] private Grabbable grabbableLid;
    [SerializeField] private GameObject crowbarLidPlacement;
    private bool inLidTrigger;
    #endregion

    #region Sound
    [SerializeField] private AudioClip hitFloorSound;
    [SerializeField] private AudioClip hitPillowSound;
    #endregion

    #region Tags
    private string floorTag = "Floor";
    private string pillowTag = "Pillow";
    private string crateLidTag = "CrateLidTrigger";
    #endregion

    void Start()
    {
        crowbarRigid = GetComponent<Rigidbody>();
        crowbarRigid.isKinematic = true;

        crowbarScript = GetComponent<Crowbar>();
        crowbarScript.enabled = false;

        grabbableLid.enabled = false;
        crowbarLidPlacement.SetActive(false);
    }

    void Update()
    {
        velocity = crowbarRigid.velocity.magnitude;                                                  //sets the velocity 
    }

    private void OnTriggerEnter(Collider other)
    {
        //If Crowbar hits Floor 
        if (other.CompareTag(floorTag) && velocity > 2f)
        {
            SoundManager.Instance.PlaySoundAtPoint(hitFloorSound, transform.position, 0.01f);   //Play hitFloorSound
            Guard.alertedGuard = true;                                                          //Alert Guard    
        }
        //If Crowbar hits Pillow
        else if (other.CompareTag(pillowTag) && velocity > 2f)
        {
            SoundManager.Instance.PlaySoundAtPoint(hitPillowSound, transform.position, 0.01f); //Play hitPillowSound 
        }

        //If Crowbar enters the CrateLid Trigger
        if (other.CompareTag(crateLidTag))
        {
            inLidTrigger = true;
            crowbarLidPlacement.SetActive(true);                                               //Activate the Highlight object
        }

        //If another Rigidbody hits the Crowbar
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            crowbarRigid.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Deactivate the Highlight object
        if (other.CompareTag(crateLidTag))
        {
            inLidTrigger = false;
            crowbarLidPlacement.SetActive(false);
        }
    }

    /// <summary>
    /// Moves Crowbar to Highlight location - Called when Crowbar is dropped by Player's hand
    /// </summary>
    public void MoveToHighlight()
    {
        if (inLidTrigger == true)
        {
            gameObject.transform.position = crowbarLidPlacement.transform.position;
            gameObject.transform.rotation = crowbarLidPlacement.transform.rotation;
            crowbarRigid.isKinematic = true;
            crowbarLidPlacement.SetActive(false);
            grabbableLid.enabled = true;
        }
    }

    /// <summary>
    /// Removes Crowbar from Highlight location - Called when player grabs the Crowbar
    /// </summary>
    public void LeaveHighlight()
    {
        if (inLidTrigger == true)
        {
            inLidTrigger = false;
            crowbarRigid.isKinematic = false;
            crowbarLidPlacement.SetActive(false);
        }
    }
}
