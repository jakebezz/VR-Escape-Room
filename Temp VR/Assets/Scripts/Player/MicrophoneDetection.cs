using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that detects the loudness of the Microphone - credit goes to Valem Tutorials on YouTube 
/// https://youtu.be/dzD0qP8viLw 
/// </summary>
public class MicrophoneDetection : MonoBehaviour
{
    private int sampleWindow = 64;                                 
    [SerializeField] private AudioSource source;
    [SerializeField] private float loudnessSensibility = 100;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float detectionNumber = 1;                         //the smaller the number the easier the detection
    [SerializeField] private float loudness;

    private AudioClip microphoneClip;

    void Start()
    {
        MicrophoneToAudioClip();
    }

    void Update()
    {
        loudness = GetLoundnessFromMicroPhone(source.timeSamples, source.clip) * loudnessSensibility;    //Sets the loudness based on loudness from the microphone clip

        if (loudness < threshold)
        {
            loudness = 0;
        }

        //Alets Guard
        if (loudness >= detectionNumber)
        {
            Guard.alertedGuard = true;
        }
    }

    private void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    private float GetLoundnessFromMicroPhone(int clipPosition, AudioClip clip)
    {
        clipPosition = Microphone.GetPosition(Microphone.devices[0]);
        clip = microphoneClip;

        int startPosition = clipPosition - sampleWindow;

        //Stops the audio clip from starting at a negative number
        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
