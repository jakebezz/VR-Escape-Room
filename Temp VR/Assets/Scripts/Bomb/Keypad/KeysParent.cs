using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KeysParent : MonoBehaviour
{
    public Keypad keypad;                                                        //Keypad display

    [Range(0, 9)]
    public int number;                                                           //Number of this key

    public delegate void PressKey();                                             //Delegate Creation
    [NonSerialized] public PressKey pressKey;                                    //Delegate Function


    //Calls Delegate in Hand Poke Interactable Event
    public void InvokePressKey()
    {
        pressKey?.Invoke();
    }

    /// <summary>
    /// Input the Key number into the Keypad screen
    /// </summary>
    public void InputKey()
    {
        if (keypad.codeGuessed.Count < 4)
        {
            keypad.codeGuessed.Add(number);                                 //Adds this keys number to the list
            keypad.codeVisual[keypad.input].SetText(number.ToString());     //Adds the text to the codeVisual array
            keypad.input++;                                                 //Incrase input number        
        }
    }
}
