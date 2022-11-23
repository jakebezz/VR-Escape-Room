using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParts : MonoBehaviour
{
    //Parent Class with a very basic job, places the object at the Bomb Placement location
    public GameObject placementLoc;

    //Used to rotate Timer so the time faces up
    public bool isPlaced;

    //MAYBE A GLOBAL VELOCITY THING SO IF ANY OF THEM HIT THE GROUND TOO FAST IT WILL ALET GUARD
    public Rigidbody bombPartRigid;

    private string bombPlacementTag = "BombPlacement";

    protected virtual void Start()
    {
        placementLoc.SetActive(false);

        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            bombPartRigid = gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            placementLoc.SetActive(true);

            if (Input.GetKeyDown(KeyCode.K))
            {
                MoveToPlacement();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            //bombPartRigid.isKinematic = false;
            //bombPartRigid.useGravity = true;
            isPlaced = false;
            placementLoc.SetActive(false);
        }
    }

    private void MoveToPlacement()
    {
        gameObject.transform.position = placementLoc.transform.position;
        gameObject.transform.rotation = placementLoc.transform.rotation;
        bombPartRigid.isKinematic = true;
        bombPartRigid.useGravity = false;
        isPlaced = true;
    }
}
