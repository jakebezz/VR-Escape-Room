using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearKey : KeysParent
{
    private void Start()
    {
        pressKey = ClearInput;                                                      //Sets deleate to clear input
    }

    /// <summary>
    /// Rests the variables and sets the screen to display four 0's
    /// </summary>
    private void ClearInput()
    {
        keypad.codeGuessed.Clear();
        keypad.input = 0;

        for (int i = 0; i < keypad.codeVisual.Length; i++)
        {
            keypad.codeVisual[i].SetText(0.ToString());
        }
    }
}
