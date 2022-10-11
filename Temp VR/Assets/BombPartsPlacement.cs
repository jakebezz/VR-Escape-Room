using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPartsPlacement : MonoBehaviour
{
    //Temp Script. CHANGE INTO CLASS WITH CHILDREN???

    //Each have an object, a transform and a name, can ihereit these?


    [SerializeField] private Transform dynamiteLoc;
    [SerializeField] private Transform redWireLoc;
    [SerializeField] private Transform whiteWireLoc;
    [SerializeField] private Transform blueWireLoc;
    [SerializeField] private Transform timerLoc;

    [SerializeField] private GameObject dynamiteObj;
    [SerializeField] private GameObject redWireObj;
    [SerializeField] private GameObject whiteWireObj;
    [SerializeField] private GameObject blueWireObj;
    [SerializeField] private GameObject timerObj;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BombObject")
        {
            if(other.name == "Timer")
            {
                timerObj.transform.position = timerLoc.position;
            }
        }
    }
}
