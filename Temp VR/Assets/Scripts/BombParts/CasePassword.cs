using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePassword : MonoBehaviour
{
    public bool isCorrect;
    [SerializeField] private float rotationChange;
    [SerializeField] private float rotationTarget;

    [SerializeField] private KeyCode key;

    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            transform.rotation *= Quaternion.Euler(rotationChange, 0f, 0f);
        }

        if (transform.rotation == Quaternion.Euler(rotationTarget, 0f, 0f))
        {
            isCorrect = true;
        }
    }
}
