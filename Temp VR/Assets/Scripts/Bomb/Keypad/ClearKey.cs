using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearKey : KeysParent
{
    private void Start()
    {
        //Sets deleate to clear input
        pressKey = ClearInput;
    }

    //Rests the variables and sets the screen to display four 0's
    private void ClearInput()
    {
        Debug.Log("Code Cleared");
        keypad.codeGuessed.Clear();
        keypad.input = 0;

        for (int i = 0; i < keypad.codeVisual.Length; i++)
        {
            keypad.codeVisual[i].SetText(0.ToString());
        }
    }
}
