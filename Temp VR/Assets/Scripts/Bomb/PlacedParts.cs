using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedParts : MonoBehaviour
{
    [SerializeField] private BombParts[] bombParts;

    void Update()
    {
        if(CheckAllPartsPlaced() == true)
        {
            Debug.Log("All Parts Placed");
        }
    }

    private bool CheckAllPartsPlaced()
    {
        for(int i = 0; i < bombParts.Length; i++)
        {
            if(bombParts[i].isInTrigger == false)
            {
                return false;
            }
        }
        return true;
    }
}
