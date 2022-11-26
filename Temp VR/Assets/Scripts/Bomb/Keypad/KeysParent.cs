using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysParent : MonoBehaviour
{
    //Keypad display
    public Keypad keypad;

    //Number of this key
    public int number;

    //Temp input
    public KeyCode key;

    //Delegate
    public delegate void PressKey();
    public PressKey pressKey;

    private void Update()
    {
        //Invoke delegate, will change to when player presses key
        if (Input.GetKeyDown(key))
        {
            pressKey?.Invoke();
        }
    }

    //Called in Oculus input 
    public void InputKey()
    {
        if (keypad.codeGuessed.Count < 4)
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
