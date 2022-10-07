using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Used to check if player is hiding in closet
    public bool isHidden;

    private void Start()
    {
        isHidden = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hidden")
        {
            isHidden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hidden")
        {
            isHidden = false;
        }
    }
}
