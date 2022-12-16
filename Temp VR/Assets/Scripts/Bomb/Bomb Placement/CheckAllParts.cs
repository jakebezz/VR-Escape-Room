using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllParts : MonoBehaviour
{
    [SerializeField] private BombParts[] bombParts;                 //Reference to all Bomb parts

    /// <summary>
    /// Checks if all Bomb Parts have been placed - Called when a Bomb Part has been placed
    /// </summary>
    /// <returns></returns>
    public bool CheckAllPlaced()
    {
        foreach (BombParts part in bombParts)
        {
            if (part.isPlaced == false)
            {
                return false;
            }
        }
        return true;
    }
}
