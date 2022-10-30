using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePassword : MonoBehaviour
{
    public bool isCorrect;
    [SerializeField] private float rotationChange;
    [SerializeField] private float rotationTarget;

    [SerializeField] private KeyCode key;

    [SerializeField] GameObject[] ignoreObject;

    [SerializeField] Vector3 currentEulerAngles;

    private void Start()
    {
        currentEulerAngles = new Vector3(0f, 180f, 0f);
        foreach(GameObject gameObject in ignoreObject)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void Update()
    {
        if(currentEulerAngles.x >= 360f)
        {
            currentEulerAngles = new Vector3(0f, 180f, 0f);
        }
        if(Input.GetKeyDown(key))
        {
            currentEulerAngles += new Vector3(rotationChange, 0f, 0f);
        }
        if(currentEulerAngles.x == rotationTarget)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        transform.eulerAngles = currentEulerAngles;
    }
}
