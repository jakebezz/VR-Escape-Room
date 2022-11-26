using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearKey : KeysParent
{
    private void Start()
    {
        pressKey = ClearInput;
    }

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
