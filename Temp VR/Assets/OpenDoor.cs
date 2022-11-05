using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator doorAnim;

    private string doorOpen = "DoorOpen";
    private string doorClose = "DoorClose";

    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            doorAnim.Play(doorOpen, 0, 0.0f);

            Debug.Log("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            doorAnim.Play(doorClose, 0, 0.0f);

            Debug.Log("DoorClose");
        }
    }
}
