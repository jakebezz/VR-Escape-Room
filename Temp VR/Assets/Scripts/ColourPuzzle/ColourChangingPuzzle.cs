using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColourChangingPuzzle : MonoBehaviour
{
    //Event to be used when all lights are off
    [SerializeField] private UnityEvent powerOff;
    public UnityEvent powerOn;

    //Array of lights
    [SerializeField] private Light[] spotLights;

    //Array of the light emissions 
    [SerializeField] private Material[] lightEmissions;

    //Array of light ray objects
    [SerializeField] private GameObject[] lightRays;
    [SerializeField] private GameObject[] keyNumbers;

    //Enviroment objects
    [SerializeField] private GameObject[] enviromentRays;
    [SerializeField] private Material enviromentEmissions;

    //Bools to change
    public bool[] lightIsOn;

    private bool puzzleSolved = false;
    private bool spotlightEnabled = true;

    private void Start()
    {
        ResetLights();

        SetMeshEnabled();
    }

    //CHANGE THIS TO WHEN HAND INPUT IS MADE
    private void Update()
    {
        /// Light One - lightBool[0]
        /// Light Two - lightBool[1]
        /// Light Three - lightBool[2]
        /// Light Four - lightBool[3]

        //Current Code: 6 7

        //If all lights are off start the powerOff event
        if (CheckBoolArrayIsFalse() == false && puzzleSolved == false)
        {
            puzzleSolved = true;
            powerOff.Invoke();
        }
        else
        {
            //Light One
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ChangeOnOff(!lightIsOn[0], lightIsOn[1], !lightIsOn[2], lightIsOn[3]);
            }

            //Light Two
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                ChangeOnOff(lightIsOn[0], !lightIsOn[1], !lightIsOn[2], !lightIsOn[3]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                ChangeOnOff(!lightIsOn[0], !lightIsOn[1], lightIsOn[2], lightIsOn[3]);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                ChangeOnOff(!lightIsOn[0], lightIsOn[1], lightIsOn[2], !lightIsOn[3]);
            }
            SetMeshEnabled();
        }
    }

    //Function that changes the vaule of the light bools
    private void ChangeOnOff(bool one, bool two, bool three, bool four)
    {
        lightIsOn[0] = one;
        lightIsOn[1] = two;
        lightIsOn[2] = three;
        lightIsOn[3] = four;
    }

    //Checks if all bools are false
    private bool CheckBoolArrayIsFalse()
    {
        for(int i = 0; i < lightIsOn.Length; i++)
        {
            if(lightIsOn[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    //Funtion to set the light and emission enabled or disabled
    private void SetMeshEnabled()
    {
        for (int i = 0; i < lightIsOn.Length; i++)
        {
            if (lightIsOn[i] == true)
            {
                spotLights[i].enabled = true;
                lightEmissions[i].EnableKeyword("_EMISSION");
                lightRays[i].SetActive(true);
                keyNumbers[i].SetActive(true);
            }
            else
            {
                spotLights[i].enabled = false;
                lightEmissions[i].DisableKeyword("_EMISSION");
                lightRays[i].SetActive(false);
                keyNumbers[i].SetActive(false);
            }
        }
    }

    //Called in Power On Event and on Start
    public void ResetLights()
    {
        lightIsOn[0] = true;
        lightIsOn[1] = true;
        lightIsOn[2] = false;
        lightIsOn[3] = true;
    }

    //Turns on/off the enviroment lights - Called in powerOff/On event
    public void SwitchSpotLights()
    {
        if(spotlightEnabled == true)
        {
            foreach(GameObject light in enviromentRays)
            {
                light.SetActive(false);
            }
            enviromentEmissions.DisableKeyword("_EMISSION");
            spotlightEnabled = false;
        }
        else
        {
            foreach (GameObject light in enviromentRays)
            {
                light.SetActive(true);
            }
            enviromentEmissions.EnableKeyword("_EMISSION");
            spotlightEnabled = true;
        }
    }
}
