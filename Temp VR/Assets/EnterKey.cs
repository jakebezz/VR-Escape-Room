using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterKey : KeysParent
{
    private void Start()
    {
        pressKey = EnterInput;
    }

    private void EnterInput()
    {
        if (keypad.CheckPassCode() == true)
        {
            Debug.Log("Code Correct");
        }
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
