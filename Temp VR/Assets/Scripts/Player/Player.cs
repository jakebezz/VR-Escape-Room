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
    [SerializeField] private GameObject centerEyeAnchor;

    //Post Processing
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;
    private float deafaultIntensity;
    private float deafaultSmoothness;
    private float increaseIntensity = 0.1f;

    //Number Objects
    [SerializeField] private HiddenObject[] hiddenObjects;
    [SerializeField] private GameObject[] shiftButtons;

    [SerializeField] private LayerMask deafultLayerMask;
    [SerializeField] private LayerMask hiddenLayerMask;
    private bool showHidden;

    [SerializeField] private GameObject handPoses;

    [SerializeField] private float crouchHeight;

    //Tag Strings
    private string hiddenTag = "Hidden";
    private string electricTag = "ElectricTrigger";

    private void Start()
    {
        //Gets the Post Processing Effects
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        colorAdjustments.active = false;

        //Enable and Disable cameras
        mainCamera.enabled = true;

        showHidden = false;

        mainCamera.cullingMask = deafultLayerMask;

        isHidden = false;

        //Used to reset Vignette Intensity and Smoothness
        deafaultIntensity = vignette.intensity.value;
        deafaultSmoothness = vignette.smoothness.value;

        foreach (HiddenObject hidden in hiddenObjects)
        {
            hidden.gameObject.SetActive(false);
        }

        foreach (GameObject button in shiftButtons)
        {
            button.SetActive(false);
        }
    }

    private void Update()
    {
        //Player equips glasses so see hidden object
        if (Input.GetKeyDown(KeyCode.G))
        {
            EnableHidden();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseHueShift();
            Debug.Log(colorAdjustments.hueShift.value);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DecreaseHueShift();
            Debug.Log(colorAdjustments.hueShift.value);
        }

    }

    //Player takes damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(hiddenTag))
        {
            handPoses.SetActive(true);
        }

        if (other.CompareTag(electricTag))
        {
            electricDamage = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If player crouches by the window they are hidden
        if (other.CompareTag(hiddenTag))
        {
            //How far the need to crouch
            if (centerEyeAnchor.transform.localPosition.y < crouchHeight)
            {
                isHidden = true;
                Debug.Log("Player Is Hidden");
                
                //Activate vignette effect
                VignetteControl(Color.black, deafaultIntensity, deafaultSmoothness);
            }
            else
            {
                vignette.active = false;
            }
        }

        //Plays vignette effect if player is in electic trigger
        if (other.CompareTag(electricTag))
        {
            //Increase the vignette intensity overtime
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
        //Disables vigentte when player is no longer in hidden trigger
        if(other.CompareTag(hiddenTag))
        {
            vignette.active = false;
            isHidden = false;

            handPoses.SetActive(false);
        }

        //Reset variables when player is no longer in electrical trigger
        if(other.CompareTag(electricTag))
        {
            increaseIntensity = 0.1f;
            electricDamage = false;

            //Remove vignette effect over time
            StartCoroutine(RemoveVignette());
        }
    }

    //Activate vignette effect with variables
    private void VignetteControl(Color color, float intensity, float smoothness)
    {
        vignette.active = true;
        vignette.color.Override(color);
        vignette.intensity.Override(intensity);
        vignette.smoothness.Override(smoothness);
    }

    //Removes the vignette effect overtime
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

    private void ShowHiddenObjects()
    {
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            if (hiddenObjects[i].RevealHiddenObject(colorAdjustments.hueShift.value) == true)
            {
                hiddenObjects[i].gameObject.SetActive(true);
            }
            else
            {
                hiddenObjects[i].gameObject.SetActive(false);
            }
        }
    }

    private void MakeButtonsVisible(bool showButtons)
    {
        foreach (GameObject button in shiftButtons)
        {
            button.SetActive(showButtons);
        }
    }

    public void IncreaseHueShift()
    {
        if (colorAdjustments.hueShift.value < 175)
        {
            colorAdjustments.hueShift.value += 20;
            ShowHiddenObjects();
        }
    }

    public void DecreaseHueShift()
    {
        if (colorAdjustments.hueShift.value > 20)
        {
            colorAdjustments.hueShift.value -= 20;
            ShowHiddenObjects();
        }
    }

    public void EnableHidden()
    {
        colorAdjustments.active = !colorAdjustments.active;
        showHidden = !showHidden;
        mainCamera.cullingMask = hiddenLayerMask;

        MakeButtonsVisible(true);
    }

    public void DisableHidden()
    {
        colorAdjustments.active = !colorAdjustments.active;
        showHidden = !showHidden;
        mainCamera.cullingMask = deafultLayerMask;

        MakeButtonsVisible(false);
    }

}
