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

    bool[] input = new bool[4];

    bool puzzleSolved = false;

    private void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            lightBool[i] = true;
            input[i] = true;
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
            if (input[0] == true)
            {
                //Makes lightOne false, lightTwo stays the same, lightThree on and lightFour on
                ChangeOnOff(false, lightBool[1], true, true);
                input[0] = false;
            }
            else
            {
                //Inverses the lights so they can turn on and off when gesture is made
                ChangeOnOff(!lightBool[0], lightBool[1], !lightBool[2], !lightBool[3]);
                input[0] = true;
            }
            SetMeshEnabled();
        }

        //Light Two
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (input[1] == true)
            {
                ChangeOnOff(lightBool[0], false, false, false);
                input[1] = false;
            }
            else
            {
                ChangeOnOff(lightBool[0], !lightBool[1], !lightBool[2], !lightBool[3]);
                input[1] = true;
            }
            SetMeshEnabled();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (input[2] == true)
            {
                ChangeOnOff(true, false, false, lightBool[3]);
                input[2] = false;
            }
            else
            {
                ChangeOnOff(!lightBool[0], !lightBool[1], !lightBool[2], lightBool[3]);
                input[2] = true;
            }   
            SetMeshEnabled();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (input[3] == true)
            {
                ChangeOnOff(false, lightBool[1], true, false);
                input[3] = false;
            }
            else
            {
                ChangeOnOff(!lightBool[0], lightBool[1], !lightBool[2], !lightBool[3]);
                input[3] = true;
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
