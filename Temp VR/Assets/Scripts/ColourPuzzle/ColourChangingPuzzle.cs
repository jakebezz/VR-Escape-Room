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
    
    //Bools to change
    public bool[] lightIsOn;

    bool puzzleSolved = false;

    private void Start()
    {
        //This is probably pointless but gives more room for Extendibility
        for (int i = 0; i < lightIsOn.Length; i++)
        {
            if (i != 2)
            {
                lightIsOn[i] = true;
            }
            else
            {
                lightIsOn[i] = false;
            }
        }

        lightIsOn[0] = true;
        lightIsOn[1] = true;
        lightIsOn[2] = false;
        lightIsOn[3] = true;

        SetMeshEnabled();
    }

    //Maybe Change this into Parent and Children
    private void Update()
    {
        /// Light One - lightBool[0]
        /// Light Two - lightBool[1]
        /// Light Three - lightBool[2]
        /// Light Four - lightBool[3]

        //Current Code: 6 7

        //Light One
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeOnOff(!lightIsOn[0], lightIsOn[1], !lightIsOn[2], lightIsOn[3]);
            SetMeshEnabled();
            Debug.Log("6");
        }

        //Light Two
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeOnOff(lightIsOn[0], !lightIsOn[1], !lightIsOn[2], !lightIsOn[3]);
            SetMeshEnabled();
            Debug.Log("7");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeOnOff(!lightIsOn[0], !lightIsOn[1], lightIsOn[2], lightIsOn[3]);
            SetMeshEnabled();
            Debug.Log("8");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeOnOff(!lightIsOn[0], lightIsOn[1], lightIsOn[2], !lightIsOn[3]);
            SetMeshEnabled();
            Debug.Log("9");
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

    //Funtion to set the mesh enabled of the objects, is a function istead of in update to save preformance
    private void SetMeshEnabled()
    {
        for (int i = 0; i < lightIsOn.Length; i++)
        {
            if (lightIsOn[i] == true)
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
