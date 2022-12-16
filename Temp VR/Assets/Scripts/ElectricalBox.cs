using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ElectricalBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem electric;
    [SerializeField] private Grabbable doorGrabbable;

    public bool isActive = true;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        doorGrabbable.enabled = false;
    }

    /// <summary>
    /// Turn Off Power Box Sound and FX - Called in Power Off Event
    /// </summary>
    public void TurnOffPowerBox()
    {
        audioSource.enabled = false;
        doorGrabbable.enabled = true;
        isActive = false;
        electric.Pause();
    }

    /// <summary>
    /// Turn On Power Box Sound and FX - Called in Power On Event
    /// </summary>
    public void TurnOnPowerBox()
    {
        audioSource.enabled = true;
        doorGrabbable.enabled = false;
        isActive = true;
        electric.Play();
    }
}
