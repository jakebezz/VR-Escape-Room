using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneDetection : MonoBehaviour
{
    //REWATCH VIDEO AND COMMENT CODE, CHANGE IT TOO
    [SerializeField] private int sampleWindow = 64;
    [SerializeField] private AudioSource source;
    [SerializeField] private float loudnessSensibility = 100;
    [SerializeField] private float threshold = 0.1f;
    //the smaller the number the easier the detection
    [SerializeField] private float detectionNumber = 1;

    private AudioClip microphoneClip;

    //Access to Guard Class
    private Guard guard;
    [SerializeField] private GameObject guardObj;
    //Used to duplicated the location of the player, stops the guard from following the player
    [SerializeField] private GameObject playerLocation;

    void Start()
    {
        MicrophoneToAudioClip();

        guard = guardObj.GetComponent<Guard>();
    }

    void Update()
    {
        float loudness = GetLoundnessFromMicroPhone(source.timeSamples, source.clip) * loudnessSensibility;

        if(loudness < threshold)
        {
            loudness = 0;
        }

        if(loudness > detectionNumber)
        {
            Debug.Log("Guard Heard");

            //Creates the clone of the players location
            GameObject playerLocationClone;
            playerLocationClone = Instantiate(playerLocation, playerLocation.transform.position, transform.rotation);

            //Sends guard to location of the cloned location
            guard.alertedGuards = true;
            guard.alertedLocation = playerLocationClone.transform;


        }
    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoundnessFromMicroPhone(int clipPosition, AudioClip clip)
    {
        clipPosition = Microphone.GetPosition(Microphone.devices[0]);
        clip = microphoneClip;

        int startPosition = clipPosition - sampleWindow;
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
