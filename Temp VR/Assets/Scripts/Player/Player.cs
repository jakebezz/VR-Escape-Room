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

    //Cameras
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera hiddenCamera;
    [SerializeField] private GameObject trackingSpace;

    //Post Processing
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private float deafaultIntensity;
    private float deafaultSmoothness;
    private float increaseIntensity = 0.1f;

    //Tag Strings
    private string hiddenTag = "Hidden";
    private string electricTag = "ElectricTrigger";

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        mainCamera.enabled = true;
        hiddenCamera.enabled = false;

        isHidden = false;

        deafaultIntensity = vignette.intensity.value;
        deafaultSmoothness = vignette.smoothness.value;
    }

    private void Update()
    {
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
                mainCamera.enabled = false;
                hiddenCamera.enabled = true;
                Debug.Log("Can See Hidden Objects");
            }
            else
            {
                mainCamera.enabled = true;
                hiddenCamera.enabled = false;
            }

            Debug.Log("Hue Shift Value: " + colorAdjustments.hueShift.value);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(electricTag))
        {
            electricDamage = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(hiddenTag))
        {
            if (trackingSpace.transform.localPosition.y < -0.5)
            {
                isHidden = true;
                Debug.Log("Player Is Hidden");

                VignetteControl(Color.black, deafaultIntensity, deafaultSmoothness);
            }
            else
            {
                vignette.active = false;
            }
        }

        if (other.CompareTag(electricTag))
        {
            increaseIntensity += 0.01f;
            if(electricDamage == true)
            {
                VignetteControl(Color.cyan, increaseIntensity, 0.5f);
                Debug.Log("Player take damage");
            }
            else
            {
                Debug.Log("Player take no damage");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(hiddenTag))
        {
            vignette.active = false;
            isHidden = false;
        }

        if(other.CompareTag(electricTag))
        {
            increaseIntensity = 0.1f;
            electricDamage = false;

            StartCoroutine(RemoveVignette());
        }
    }

    private void VignetteControl(Color color, float intensity, float smoothness)
    {
        vignette.active = true;
        vignette.color.Override(color);
        vignette.intensity.Override(intensity);
        vignette.smoothness.Override(smoothness);
    }

    private IEnumerator RemoveVignette()
    {
        do 
        {
            yield return new WaitForSeconds(0.1f);
            vignette.intensity.value -= 0.2f;
        } 
        while (vignette.intensity.value >= 0.2);

        vignette.active = false;
    }
}
