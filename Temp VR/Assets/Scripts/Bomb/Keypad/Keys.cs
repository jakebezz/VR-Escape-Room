using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    //Keypad display
    [SerializeField] private Keypad keypad;

    //Number of this key
    [SerializeField] private int number;

    //Temp input
    [SerializeField] private KeyCode key;


    //MAYBE CREATE ENUM THEN USE KEYPAD SCRIPT TO GET ENUM AND INPUT THAT NUMBER

    private void Update()
    {
        if (Input.GetKeyDown(key) && keypad.codeGuessed.Count <= 4)
        {
            //Adds this keys number to the list
            keypad.codeGuessed.Add(number);
            //Adds the text to the codeVisual array
            keypad.codeVisual[keypad.input].SetText(number.ToString());
            //Incrase input number
            keypad.input++;
        }
    }
}
