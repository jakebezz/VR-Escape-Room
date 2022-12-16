using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class EnterKey : KeysParent
{
    [SerializeField] private Grabbable grabbableLid;                        //Grabbable script to be activated when correct password is inputed

    private void Start()
    {
        grabbableLid.enabled = false;

        pressKey = EnterInput;                                              //Sets delegate to Enter key
    }

    /// <summary>
    /// Checks if Passord is correct when the Enter key is pressed
    /// </summary>
    private void EnterInput()
    {
        if (CheckPassCode() == true)
        {
            grabbableLid.enabled = true;
        }

        //If code is incorrect, rest the variables and sets the screen to display four 0's
        else
        {
            keypad.codeGuessed.Clear();
            keypad.input = 0;

            for (int i = 0; i < keypad.codeVisual.Length; i++)
            {
                keypad.codeVisual[i].SetText(0.ToString());
            }
        }
    }

    /// <summary>
    /// Checks if the passcode is correct
    /// </summary>
    /// <returns></returns>
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
