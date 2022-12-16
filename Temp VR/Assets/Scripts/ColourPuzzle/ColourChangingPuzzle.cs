using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ColourChangingPuzzle : MonoBehaviour
{
    #region Inspector Only
    /// <summary>
    /// Contains information about the Light Array and the correct Password
    /// </summary>
    [Header("Puzzle Information")]
    [Multiline(6)]
    [SerializeField] private string puzzleInformation;
    [Space(10)]
    #endregion

    #region Container Lights
    [Header("Container Lights Visuals")]     
    [SerializeField] private Light[] spotLights;                                     //Array of Spot Lights       
    [SerializeField] private Material[] lightEmissions;                              //Array of the Light Emissions 
    [SerializeField] private GameObject[] lightRays;                                 //Array of Light Ray Objects
    [SerializeField] private MeshRenderer[] keyNumbers;                              //Array of Hidden Numbers for Keypad Passcode
    private bool[] lightIsOn = new bool[4];                                          //Array of Bools to determine whether Light is On or Off
    [Space(10)]
    #endregion

    #region Enviroment Lights
    [Header("Enviroment Lights Visuals")]
    [SerializeField] private GameObject[] enviromentRays;
    [SerializeField] private Material enviromentEmissions;
    private bool envirotmentLightsEnabled = true;
    [Space(10)]
    #endregion

    #region Sound
    [Header("Sounds")]
    [SerializeField] private AudioClip powerOffSound;
    [SerializeField] private AudioClip powerOnSound;
    [Space(10)]
    #endregion

    #region Unity Events
    [Header("Power Off Event")]
    [SerializeField] private UnityEvent powerOffEvent;                                                   //Event to be used when all lights are off
    [Space(10)]
    [Header("Power On Event")]
    public UnityEvent powerOnEvent;                                                                     //Event to be used when lights are turned back on
    [Space(10)]
    #endregion

    private bool puzzleSolved = false;                                                                  //Sets to True when puzzle is solved so Power Off Event cant be called repeatedly

    private void Start()
    {
        ResetLights();

        SetMeshEnabled();
    }

    /// <summary>
    /// Sets and Resets the Lights to default state - called on Start and Power On Event
    /// </summary>
    public void ResetLights()
    {
        lightIsOn[0] = true;
        lightIsOn[1] = true;
        lightIsOn[2] = false;
        lightIsOn[3] = true;
    }

    #region Change Meshes
    /// <summary>
    /// Turns on/off the Container Lights and Emissions
    /// </summary>
    private void SetMeshEnabled()
    {
        for (int i = 0; i < lightIsOn.Length; i++)
        {
            if (lightIsOn[i] == true)
            {
                spotLights[i].enabled = true;
                lightEmissions[i].EnableKeyword("_EMISSION");
                lightRays[i].SetActive(true);
                keyNumbers[i].enabled = true;
            }
            else
            {
                spotLights[i].enabled = false;
                lightEmissions[i].DisableKeyword("_EMISSION");
                lightRays[i].SetActive(false);
                keyNumbers[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// Turns on/off the Enviroment Lights - Called in powerOff/On event
    /// </summary>
    public void SetEnvriomentLights()
    {
        if (envirotmentLightsEnabled == true)
        {
            foreach (GameObject light in enviromentRays)
            {
                light.SetActive(false);
            }
            enviromentEmissions.DisableKeyword("_EMISSION");
            envirotmentLightsEnabled = false;
        }
        else
        {
            foreach (GameObject light in enviromentRays)
            {
                light.SetActive(true);
            }
            enviromentEmissions.EnableKeyword("_EMISSION");
            envirotmentLightsEnabled = true;
        }
    }
    #endregion

    #region Check Lights
    /// <summary>
    /// Returns True if all Lights are Off
    /// </summary>
    /// <returns></returns>
    private bool CheckBoolArrayIsFalse()
    {
        for (int i = 0; i < lightIsOn.Length; i++)
        {
            if (lightIsOn[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// //If all Lights are Off start the Power Off Event - Called when Hand Pose is made
    /// </summary>
    private void CheckPowerStatus()
    {
        if (CheckBoolArrayIsFalse() == false && puzzleSolved == false)
        {
            puzzleSolved = true;
            powerOffEvent.Invoke();
        }
    }
    #endregion

    #region Change Lights Functions

    /// <summary>
    /// Different Function called based on Hand Pose made
    /// </summary>
    private void ChangeOnOff(bool one, bool two, bool three, bool four)
    {
        lightIsOn[0] = one;
        lightIsOn[1] = two;
        lightIsOn[2] = three;
        lightIsOn[3] = four;
    }

    public void ChangeRed()
    {
        ChangeOnOff(!lightIsOn[0], lightIsOn[1], !lightIsOn[2], lightIsOn[3]);
        SetMeshEnabled();
        CheckPowerStatus();
    }

    public void ChangeYellow()
    {
        ChangeOnOff(lightIsOn[0], !lightIsOn[1], !lightIsOn[2], !lightIsOn[3]);
        SetMeshEnabled();
        CheckPowerStatus();
    }

    public void ChangeBlue()
    {
        ChangeOnOff(!lightIsOn[0], !lightIsOn[1], lightIsOn[2], lightIsOn[3]);
        SetMeshEnabled();
        CheckPowerStatus();
    }

    public void ChangeWhite()
    {
        ChangeOnOff(!lightIsOn[0], lightIsOn[1], lightIsOn[2], !lightIsOn[3]);
        SetMeshEnabled();
        CheckPowerStatus();
    }
    #endregion

    #region Play Sound Functions
    public void PlayPowerOffSound()
    {
        SoundManager.Instance.PlaySoundAtPoint(powerOffSound, transform.position, 1f);
    }

    public void PlayPowerOnSound()
    {
        SoundManager.Instance.PlaySoundAtPoint(powerOnSound, transform.position, 1f);
    }
    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            powerOffEvent?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            powerOnEvent?.Invoke();
        }
    }
}
