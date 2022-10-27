using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPartsPlacement : MonoBehaviour
{
    public Transform placementLoc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BombPlacement")
        {
            gameObject.transform.position = placementLoc.position;
        }
    }
}
