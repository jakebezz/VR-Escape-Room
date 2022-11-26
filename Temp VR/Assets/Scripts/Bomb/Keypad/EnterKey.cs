using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterKey : KeysParent
{
    private void Start()
    {
        //Sets delegate to Enter key
        pressKey = EnterInput;
    }

    //Checks if the code inputed is correct
    private void EnterInput()
    {
        if (keypad.CheckPassCode() == true)
        {
            Debug.Log("Code Correct");
        }
        //If code is incorrect, rest the variables and sets the screen to display four 0's
        else
        {
            Debug.Log("Code Incorrect");
            keypad.codeGuessed.Clear();
            keypad.input = 0;

            for (int i = 0; i < keypad.codeVisual.Length; i++)
            {
                keypad.codeVisual[i].SetText(0.ToString());
            }
        }
    }
}
