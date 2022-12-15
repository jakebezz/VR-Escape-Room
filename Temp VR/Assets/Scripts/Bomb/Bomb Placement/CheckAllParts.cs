using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllParts : MonoBehaviour
{
    [SerializeField] private BombParts[] bombParts;

    public bool CheckAllPlaced()
    {
        foreach(BombParts part in bombParts)
        {
            if(part.isPlaced == false)
            {
                return false;
            }
        }
        return true;
    }
}
