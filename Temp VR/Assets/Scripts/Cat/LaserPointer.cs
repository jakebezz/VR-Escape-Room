using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private CatMove cat;

    private LineRenderer laserLine;
    [SerializeField] private float maxLineLength;

    private bool floorHit;

    private RaycastHit hit;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
        floorHit = false;
    }

    private void Update()
    {
        //Enables/Disables the laser pointer
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (laserLine.enabled == false)
            {
                laserLine.enabled = true;
            }
            else
            {
                laserLine.enabled = false;
            }
        }

        if (laserLine.enabled == true)
        {
            //Sets the start position of the laser to the game object position
            laserLine.SetPosition(0, transform.position);
            SetRayCast();
        }
    }

    private void SetRayCast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                //If the raycast hits the CatFloor collider, set floorHit to true
                if (hit.collider.gameObject.tag == "CatFloor")
                {
                    floorHit = true;
                }
                //If the collider hit is not the CatFloor, set floorHit to false
                else
                {
                    floorHit = false;
                }
                //Sets the end point of laser to the hit point
                laserLine.SetPosition(1, hit.point);
            }
        }
        //If there is nothing to RayCast to, set floorHit to false and end point of laser to the max length
        else
        {
            floorHit = false;
            laserLine.SetPosition(1, transform.forward * maxLineLength);
        }

        //Move Cat only if the laser it hitting the floor
        if (floorHit == true)
        {
            StartCoroutine(DelayCatMove(0.2f, hit.point));
        }
        else if (floorHit == false)
        {
            StartCoroutine(DelayCatMove(0.5f, cat.catAgent.gameObject.transform.position));
        }
    }

    
    /// Delays the Cat movement to make it feel more natual
    /// Stops the Cat from running to the ray cast instantly
    /// Stops Cat from always moving to the last place the laser hit, for example if the player moved it off the platform, the cat would go to the edge of the platform
    /// If the player stops pointing the laser at the floor, the Cat will wait half a second and stay where they are
    IEnumerator DelayCatMove(float delay, Vector3 destination)
    {
        yield return new WaitForSeconds(delay);

        cat.catAgent.destination = destination;
    }
}
