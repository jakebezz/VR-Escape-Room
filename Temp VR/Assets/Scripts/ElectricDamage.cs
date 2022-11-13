using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDamage : MonoBehaviour
{
    public bool damagePlayer = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(damagePlayer == true)
            {
                Debug.Log("Damage Player");
            }
            else
            {
                Debug.Log("Dont Damage Player");
            }
        }
    }
}
