using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] private Keypad keypad;

    [SerializeField] private int number;

    [SerializeField] private KeyCode key;

    private void Update()
    {
        if (Input.GetKeyDown(key) && keypad.codeGuessed.Count <= 4)
        {
            keypad.codeGuessed.Add(number);
            keypad.codeVisual[keypad.input].SetText(number.ToString());
            keypad.input++;
        }
    }
}
