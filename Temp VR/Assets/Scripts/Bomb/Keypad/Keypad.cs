using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public int[] keyCode = new int[4];                                              //The correct code to be compared against
    public List<int> codeGuessed;                                                   //List of what the player has guessed, will be cleared if they guess wrong 

    [NonSerialized] public int input = 0;                                           //Input number of the player, will change which part of the array will display - e.g if input == 1, the 3rd number on the display will be to next input
    [NonSerialized] public TextMeshPro[] codeVisual = new TextMeshPro[4];           //Visual of the code on the pad


    /// <summary>
    /// Sets the Keypad password
    /// </summary>
    private void Start()
    {
        keyCode[0] = 3;
        keyCode[1] = 5;
        keyCode[2] = 2;
        keyCode[3] = 9;
    }
}
