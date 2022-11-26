using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    //Variables for event system
    [SerializeField] private ParticleSystem electric;
    [SerializeField] private bool canBeOpened = false;

    //Function to be called in event
    public void TurnOffPowerBox()
    {
        electric.Pause();
        canBeOpened = true;
    }

    public void TurnOnPowerBox()
    {
        electric.Play();
        canBeOpened = false;
    }
}
