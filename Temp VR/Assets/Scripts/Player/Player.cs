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
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
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


        if(Input.GetKeyDown(KeyCode.G))
        {
            if (colorAdjustments.active == true)
            {
                colorAdjustments.active = false;
            }
            else
            {
                colorAdjustments.active = true;
            }
        }

        if(colorAdjustments.active == true)
        {
            if(Input.GetMouseButton(0))
            {
                colorAdjustments.hueShift.value++;
            }

            if(Input.GetMouseButton(1))
            {
                colorAdjustments.hueShift.value--;
            }

            if((colorAdjustments.hueShift.value > 50  && colorAdjustments.hueShift.value < 60) || (colorAdjustments.hueShift.value < -50 && colorAdjustments.hueShift.value > -60))
            {
                Debug.Log("Can See Hidden Objects");
            }



            Debug.Log("Hue Shift Value: " + colorAdjustments.hueShift.value);
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
