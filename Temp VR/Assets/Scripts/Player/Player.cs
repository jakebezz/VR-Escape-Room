using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public bool isHidden;                                                           //Check if player is crouching by window
    public bool electricDamage = true;                                              //Check if player takes damage from Electrical box

    [SerializeField] private GameObject handPoses;
    [SerializeField] private float crouchHeight;
    [SerializeField] private ElectricalBox electricalBox;

    #region Camera
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;                                    //Main Camera
    [SerializeField] private GameObject centerEyeAnchor;                           //VR Tracking space
    [SerializeField] private LayerMask deafultLayerMask;
    [SerializeField] private LayerMask hiddenLayerMask;
    private bool showHidden;
    [Space(10)]
    #endregion

    #region Post Processing
    [Header("Post Processing")]
    [SerializeField] private Volume volume;                                         //Global Volume
    private Vignette vignette;                                      
    private ColorAdjustments colorAdjustments;
    private float deafaultIntensity;                                                //Deafult Intensity of Vignette effect
    private float deafaultSmoothness;                                               //Deafult Smoothness of Vignette effect
    private float increaseIntensity = 0.1f;                                         //Increase intensity used while player is taking damage
    #endregion

    #region Hidden Objects
    [Header("Hidden Objects")]
    [SerializeField] private HiddenObject[] hiddenObjects;                          //Hidden Objects
    [SerializeField] private GameObject[] shiftButtons;                             //Buttons to change Hue Shift
    #endregion

    #region Tags
    private string hiddenTag = "Hidden";
    private string electricTag = "ElectricTrigger";
    #endregion

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);                              //Gets the Vignette effect
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);              //Gets the ColorAdjustments effect

        mainCamera.cullingMask = deafultLayerMask;

        colorAdjustments.active = false;
        showHidden = false;
        isHidden = false;

        deafaultIntensity = vignette.intensity.value;                              //Set deafult intensity
        deafaultSmoothness = vignette.smoothness.value;                            //Set deafult smoothness

        handPoses.SetActive(false);

        //Hide hidden objects
        foreach (HiddenObject hidden in hiddenObjects)
        {
            hidden.gameObject.SetActive(false);
        }

        //Hide the Hue Shift buttons
        foreach (GameObject button in shiftButtons)
        {
            button.SetActive(false);
        }
    }

    #region Trigger Control
    private void OnTriggerEnter(Collider other)
    {
        //Enable Hand Poses when player is near window
        if (other.CompareTag(hiddenTag))
        {
            handPoses.SetActive(true);
        }

        //Player takes damage while in electrial trigger
        if (other.CompareTag(electricTag) && electricalBox.isActive == true)
        {
            electricDamage = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If player crouches by the window player is hidden
        if (other.CompareTag(hiddenTag))
        {
            if (centerEyeAnchor.transform.localPosition.y < crouchHeight)                    //How far the player need to crouch
            {
                isHidden = true;
                
                VignetteControl(Color.black, deafaultIntensity, deafaultSmoothness);        //Activate vignette effect
            }
            else
            {
                vignette.active = false;
            }
        }

        //Plays vignette effect if player is in electic trigger
        if (other.CompareTag(electricTag) && electricalBox.isActive == true)
        {
            increaseIntensity += 0.01f;                                                     //Increase the vignette intensity overtime
            if (electricDamage == true)
            {
                VignetteControl(Color.cyan, increaseIntensity, 0.5f);

                if (vignette.intensity == 1)
                {
                    GameManager.Instance.KilledByElectricBox();
                }
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

            StartCoroutine(RemoveVignette());
        }
    }
    #endregion

    #region Vignette
    /// <summary>
    /// Activate Vignette effect with variables
    /// </summary>
    /// <param name="color"></param>
    /// <param name="intensity"></param>
    /// <param name="smoothness"></param>
    private void VignetteControl(Color color, float intensity, float smoothness)
    {
        vignette.active = true;
        vignette.color.Override(color);
        vignette.intensity.Override(intensity);
        vignette.smoothness.Override(smoothness);
    }

    /// <summary>
    /// Remove Vignette effect over time
    /// </summary>
    /// <returns></returns>
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
    #endregion

    #region Hidden Objects
    /// <summary>
    /// Shows Hidden objects if their Hue Shift range is viewable
    /// </summary>
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

    /// <summary>
    /// Shows Buttons when glasses are equiped
    /// </summary>
    /// <param name="showButtons"></param>
    private void MakeButtonsVisible(bool showButtons)
    {
        foreach (GameObject button in shiftButtons)
        {
            button.SetActive(showButtons);
        }
    }

    /// <summary>
    /// Increases Hue Shift when Button Pressed
    /// </summary>
    public void IncreaseHueShift()
    {
        if (colorAdjustments.hueShift.value < 175)
        {
            colorAdjustments.hueShift.value += 20;
            ShowHiddenObjects();
        }
    }

    /// <summary>
    /// Decreases Hue Shift when Button Pressed
    /// </summary>
    public void DecreaseHueShift()
    {
        if (colorAdjustments.hueShift.value > 20)
        {
            colorAdjustments.hueShift.value -= 20;
            ShowHiddenObjects();
        }
    }

    /// <summary>
    /// Enables player to see Hidden Objects when Glasses are equipped
    /// </summary>
    public void EnableHidden()
    {
        colorAdjustments.active = !colorAdjustments.active;
        showHidden = !showHidden;
        mainCamera.cullingMask = hiddenLayerMask;

        MakeButtonsVisible(true);
    }

    /// <summary>
    /// Disable Hidden Objects when Glasses are un-equipped
    /// </summary>
    public void DisableHidden()
    {
        colorAdjustments.active = !colorAdjustments.active;
        showHidden = !showHidden;
        mainCamera.cullingMask = deafultLayerMask;

        MakeButtonsVisible(false);
    }
    #endregion
}
