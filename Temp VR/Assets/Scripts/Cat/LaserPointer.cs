using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LaserPointer : MonoBehaviour
{
    //Cat Nav agent to move
    [SerializeField] private CatMove cat;

    //Variables for the Linerenderer
    private LineRenderer laserLine;
    [SerializeField] private float maxLineLength;

    //Used to check if laser is hitting the floor
    private bool floorHit;

    //Raycast
    private RaycastHit laserHit;

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
        //Set raycast to the hit returned from RayCastFromObject
        laserHit = GameManager.Instance.RaycastFromObject(gameObject);

        if (laserHit.collider)
        {
            //If the raycast hits the CatFloor collider, set floorHit to true
            if (laserHit.collider.gameObject.tag == "CatFloor")
            {
                floorHit = true;
            }
            //If the collider hit is not the CatFloor, set floorHit to false
            else
            {
                floorHit = false;
            }
            //Sets the end point of laser to the hit point
            laserLine.SetPosition(1, laserHit.point);
        }
        //If there is nothing to RayCast to, set floorHit to false and end point of laser to the max length
        else if(!laserHit.collider)
        {
            floorHit = false;
            laserLine.SetPosition(1, transform.forward * maxLineLength);
        }

        //Move Cat only if the laser it hitting the floor
        if (floorHit == true)
        {
            StartCoroutine(DelayCatMove(0.2f, laserHit.point));
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
