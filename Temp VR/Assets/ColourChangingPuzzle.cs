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
    public bool[] lightBool = new bool[4];

    bool inputOne = true;
    bool inputTwo = true;
    bool inputThree = true;
    bool inputFour = true;

    bool puzzleSolved = false;

    private void Start()
    {
        for(int i = 0; i < lightBool.Length; i++)
        {
            lightBool[i] = true;
        }
    }

    //Maybe Change this into Parent and Children
    private void Update()
    {
        /// Light One - lightBool[0]
        /// Light Two - lightBool[1]
        /// Light Three - lightBool[2]
        /// Light Four - lightBool[3]

        //Light One
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (inputOne == true)
            {
                //Makes lightOne false, lightTwo stays the same, lightThree on and lightFour on
                ChangeOnOff(false, lightBool[1], true, true);
                inputOne = false;
            }
            else
            {
                //Inverses the lights so they can turn on and off when gesture is made
                ChangeOnOff(true, lightBool[1], false, false);
                inputOne = true;
            }
            SetMeshEnabled();
        }

        //Light Two
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (inputTwo == true)
            {
                ChangeOnOff(lightBool[0], false, false, false);
                inputTwo = false;
            }
            else
            {
                ChangeOnOff(lightBool[0], true, true, true);
                inputTwo = true;
            }
            SetMeshEnabled();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (inputThree == true)
            {
                ChangeOnOff(true, false, false, lightBool[3]);
                inputThree = false;
            }
            else
            {
                ChangeOnOff(false, true, true, lightBool[3]);
                inputThree = true;
            }   
            SetMeshEnabled();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (inputFour == true)
            {
                ChangeOnOff(false, lightBool[1], true, false);
                inputFour = false;
            }
            else
            {
                ChangeOnOff(true, lightBool[1], false, true);
                inputFour = true;
            }
            SetMeshEnabled();
        }

        //If all lights are off start the powerOff event
        if (CheckBoolArrayIsFalse() == false && puzzleSolved == false)
        {
            puzzleSolved = true;
            powerOff.Invoke();
        }
    }

    //Function that changes the vaule of the light bools
    private void ChangeOnOff(bool one, bool two, bool three, bool four)
    {
        lightBool[0] = one;
        lightBool[1] = two;
        lightBool[2] = three;
        lightBool[3] = four;
    }

    private bool CheckBoolArrayIsFalse()
    {
        for(int i = 0; i < lightBool.Length; i++)
        {
            if(lightBool[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    //Funtion to set the mesh enabled of the objects, is a function istead of in update to save preformance
    private void SetMeshEnabled()
    {
        for (int i = 0; i < lightBool.Length; i++)
        {
            if (lightBool[i] == true)
            {
                lightMeshes[i].enabled = true;
            }
            else
            {
                lightMeshes[i].enabled = false;
            }
        }
    }
}
