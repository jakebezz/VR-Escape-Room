using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Used to check if player is hiding in closet
    public bool isHidden;

    //Add Damage Effect
    public bool electricDamage = true;

    private void Start()
    {
        isHidden = false;
    }

    //Used to check if player is in or out of closet, may delete this
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hidden"))
        {
            isHidden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Hidden"))
        {
            isHidden = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "ElectricTrigger")
        {
            if(electricDamage == true)
            {
                Debug.Log("Player take damage");
            }
            else
            {
                Debug.Log("Player take no damage");
            }
        }
    }
}
