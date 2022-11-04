using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColourChangingPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] lightObjects;

    //Event
    [SerializeField] private UnityEvent powerOff;
    
    //Currently Code is 7 9 7
    //Bools to change
    public bool lightOne;
    public bool lightTwo;
    public bool lightThree;
    public bool lightFour;

    private void Start()
    {
        lightOne = true;
        lightTwo = true;
        lightThree = true;
        lightFour = true;
    }


    private void Update()
    {
        //Need way to reverse these
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeOnOff(lightObjects[0], false, lightTwo, true, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeOnOff(lightObjects[1], lightOne, false, false, false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeOnOff(lightObjects[2], true, false, false, lightFour);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeOnOff(lightObjects[3], false, lightTwo, true, false);
        }


        if ((lightOne == false) && (lightTwo == false) && (lightThree == false) && (lightFour == false))
        {
            powerOff.Invoke();
        }
    }

    //This could be delegate????

    public void ChangeOnOff(GameObject lightToChange, bool one, bool two, bool three, bool four)
    {
        lightOne = one;
        lightTwo = two;
        lightThree = three;
        lightFour = four;

        lightToChange.SetActive(false);
    }
}
