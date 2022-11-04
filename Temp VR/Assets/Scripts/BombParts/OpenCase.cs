using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCase : MonoBehaviour
{
    [SerializeField] private CasePassword[] passParts;
    private bool hasOpened;

    private void Update()
    {
        if(CheckPassword() == true && hasOpened == false)
        {
            transform.Rotate(-90f, 0f, 0f);
            hasOpened = true;
        }
    }

    private bool CheckPassword()
    {
        foreach(CasePassword password in passParts)
        {
            if(password.isCorrect == false)
            {
                return false;
            }
        }
        return true;
    }
}
