using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] private GameObject bombLid;

    //The correct code
    [SerializeField] private int[] keyCode = new int[4];

    //Visual of the code on the pad
    public TextMeshPro[] codeVisual = new TextMeshPro[4];

    //List of what the player has guessed, will be cleared if they guess wrong 
    public List<int> codeGuessed;

    //Input number of the player, will change which part of the array will display
    public int input = 0;

    private void Start()
    {
        //Sets the keycode numbers
        keyCode[0] = 3;
        keyCode[1] = 5;
        keyCode[2] = 2;
        keyCode[3] = 9;
    }

    private void Update()
    {
        //If the players next guess is the 5th guess, clear list and reset input
        if(codeGuessed.Count > 4)
        {
            Debug.Log("Code Cleared or Denied");
            codeGuessed.Clear();
            input = 0;

            for (int i = 0; i < codeVisual.Length; i++)
            {
                codeVisual[i].SetText(0.ToString());
            }
        }

        if(CheckPassCode() == false)
        {
            Debug.Log("Password is False");
        }
        else if(CheckPassCode() == true)
        {
            Debug.Log("Password is True");
            Debug.Log(bombLid.name + " can be opened");
        }
    }

    //Checks if the passcode is correct
    private bool CheckPassCode()
    {
        //If the list is empty, return false
        if (codeGuessed.Count == 0)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < keyCode.Length; i++)
            {
                if (codeGuessed[i] != keyCode[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
