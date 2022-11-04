using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColourChangingPuzzle : MonoBehaviour
{
    //Event to be used when all lights are off
    [SerializeField] private UnityEvent powerOff;

    //Meshes of cubes - CHANGE INTO LIGHTS
    [SerializeField] private MeshRenderer[] lightMeshes;
    
    //Currently Code is 7 9 7
    //Bools to change
    public bool lightOne;
    public bool lightTwo;
    public bool lightThree;
    public bool lightFour;

    private void Start()
    {
        //Set all bools to true
        lightOne = true;
        lightTwo = true;
        lightThree = true;
        lightFour = true;
    }


    private void Update()
    {
        //Light One
        if(lightOne == true)
        {
            lightMeshes[0].enabled = true;
        }
        else
        {
            lightMeshes[0].enabled = false;
        }
        //Light Two
        if (lightTwo == true)
        {
            lightMeshes[1].enabled = true;
        }
        else
        {
            lightMeshes[1].enabled = false;
        }
        //Light Three
        if (lightThree == true)
        {
            lightMeshes[2].enabled = true;
        }
        else
        {
            lightMeshes[2].enabled = false;
        }
        //Light Four
        if (lightFour == true)
        {
            lightMeshes[3].enabled = true;
        }
        else
        {
            lightMeshes[3].enabled = false;
        }


        //Light One
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (lightOne == true || lightThree == false || lightFour == false)
            {
                //Makes lightOne false, lightTwo stays the same, lightThree on and lightFour on
                ChangeOnOff(false, lightTwo, true, true);
            }
            else
            {
                //Inverses the lights so they can turn on and off when gesture is made
                ChangeOnOff(!lightOne, lightTwo, !lightThree, !lightFour);
            }
        }

        //Light Two
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (lightTwo == true || lightThree == true || lightFour == true)
            {
                ChangeOnOff(lightOne, false, false, false);
            }
            else
            {
                ChangeOnOff(lightOne, !lightTwo, !lightThree, !lightFour);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (lightOne == false || lightTwo == true || lightThree == true)
            {
                ChangeOnOff(true, false, false, lightFour);
            }
            else
            {
                ChangeOnOff(!lightOne, !lightTwo, !lightThree, lightFour);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (lightOne == true || lightThree == false || lightFour == true)
            {
                ChangeOnOff(false, lightTwo, true, false);
            }
            else
            {
                ChangeOnOff(!lightOne, lightTwo, !lightThree, !lightFour);
            }
        }

        //If all lights are off start the powerOff event
        if ((lightOne == false) && (lightTwo == false) && (lightThree == false) && (lightFour == false))
        {
            powerOff.Invoke();
        }
    }

    //Function that changes the vaule of the light bools
    public void ChangeOnOff(bool one, bool two, bool three, bool four)
    {
        lightOne = one;
        lightTwo = two;
        lightThree = three;
        lightFour = four;
    }
}
