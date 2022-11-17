using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    //Used to check if player is hiding in closet
    public bool isHidden;

    //Add Damage Effect
    public bool electricDamage = true;

    [SerializeField] private GameObject trackingSpace;

    [SerializeField] private Volume volume;
    private Vignette vignette;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }

    private void Update()
    {
        if(trackingSpace.transform.localPosition.y < -0.31)
        {
            isHidden = true;
            Debug.Log("Player Is Hidden");

            vignette.active = true;
        }
        else
        {
            vignette.active = false;
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
