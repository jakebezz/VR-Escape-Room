using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllParts : MonoBehaviour
{
    [SerializeField] private BombParts[] bombParts;

    private void Update()
    {
        //Change input
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(CheckAllPlaced() == true)
            {
                Debug.Log("End Game");
            }
            else
            {
                Debug.Log("Not All Parts Placed");
            }
        }
    }

    private bool CheckAllPlaced()
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
