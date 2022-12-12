using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class EnterKey : KeysParent
{
    [SerializeField] private Grabbable grabbableLid;

    private void Start()
    {
        grabbableLid.enabled = false;
        //Sets delegate to Enter key
        pressKey = EnterInput;
    }

    //Checks if the code inputed is correct
    private void EnterInput()
    {
        if (CheckPassCode() == true)
        {
            Debug.Log("Code Correct");
            grabbableLid.enabled = true;
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

    //Checks if the passcode is correct
    private bool CheckPassCode()
    {
        //If the list is empty, return false
        if (keypad.codeGuessed.Count == 0)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < keypad.keyCode.Length; i++)
            {
                //If one of the number is false, return false
                if (keypad.codeGuessed[i] != keypad.keyCode[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
