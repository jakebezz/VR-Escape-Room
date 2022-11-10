using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] private int[] keyCode = new int[4];
    public TextMeshPro[] codeVisual = new TextMeshPro[4];

    public List<int> codeGuessed;

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
            Debug.Log("Password Is False");
        }
        else if(CheckPassCode() == true)
        {
            Debug.Log("Password is True");
        }
    }

    private bool CheckPassCode()
    {
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
