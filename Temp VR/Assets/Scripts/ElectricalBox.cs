using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour
{
    //Variables for event system
    [SerializeField] private ParticleSystem electric;
    [SerializeField] private bool canBeOpened = false;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Function to be called in event
    public void TurnOffPowerBox()
    {
        audioSource.enabled = false;
        electric.Pause();
        canBeOpened = true;
    }

    public void TurnOnPowerBox()
    {
        audioSource.enabled = true;
        electric.Play();
        canBeOpened = false;
    }
}
