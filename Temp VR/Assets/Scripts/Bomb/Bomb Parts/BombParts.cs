using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParts : MonoBehaviour
{
    //Parent Class with a very basic job, places the object at the Bomb Placement location
    public Transform placementLoc;
    //Used to rotate Timer so the time faces up
    public bool isInTrigger;

    //MAYBE A GLOBAL VELOCITY THING SO IF ANY OF THEM HIT THE GROUND TOO FAST IT WILL ALET GUARD
    public Rigidbody bombPartRigid;

    private List<GameObject> bombParts;

    private string bombPlacementTag = "BombPlacement";

    private void Start()
    {
        foreach(Transform child in transform)
        {
            bombParts.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            gameObject.transform.position = placementLoc.position;
            isInTrigger = true;
            bombPartRigid.isKinematic = true;
            bombPartRigid.useGravity = false;

            if (CheckAllPartsPlaced() == true)
            {
                Debug.Log("All Parts Placed");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            isInTrigger = false;
            bombPartRigid.isKinematic = false;
            bombPartRigid.useGravity = true;
        }
    }

    private bool CheckAllPartsPlaced()
    {
        for (int i = 0; i < bombParts.Count; i++)
        {
            if (bombParts[i].GetComponent<BombParts>().isInTrigger == false)
            {
                return false;
            }
        }
        return true;
    }
}
