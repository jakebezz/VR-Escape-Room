using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BombParts : MonoBehaviour
{
    [Header("Placement References")]
    [SerializeField] private CheckAllParts checkPlacement;                                  //Reference to Check if all parts are placed
    [SerializeField] private GameObject hologram;                                                             //HologramObject to move Bomb part to

    [NonSerialized] public bool isPlaced;                                                   //Check if Object is placed
    [NonSerialized] public Rigidbody bombPartRigid;                                         //Rigidbody of the Bomb Parts
    private string bombPlacementTag = "BombPlacement";                                      //Tag to compare

    /// <summary>
    /// Virtual start iherited by the children, deactivates the placement hologram for the object, gets the rigidbody from the object
    /// </summary>
    protected virtual void Start()
    {
        hologram.SetActive(false);

        bombPartRigid = gameObject.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Activate the hologram when object enters box
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            hologram.SetActive(true);

            MoveToPlacement();

            //Checks if all parts have been placed when an object is placed
            if (checkPlacement.CheckAllPlaced() == true)
            {
                GameManager.Instance.PlacedAllBombParts();

            }
        }
    }

    /// <summary>
    /// Resets Variables when exiting trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(bombPlacementTag))
        {
            bombPartRigid.isKinematic = false;
            bombPartRigid.useGravity = true;
            isPlaced = false;
            hologram.SetActive(false);
        }
    }

    /// <summary>
    /// Moves the object to hologram location
    /// </summary>
    private void MoveToPlacement()
    {
        gameObject.transform.position = hologram.transform.position;
        gameObject.transform.rotation = hologram.transform.rotation;
        bombPartRigid.isKinematic = true;
        bombPartRigid.useGravity = false;
        isPlaced = true;
    }
}
