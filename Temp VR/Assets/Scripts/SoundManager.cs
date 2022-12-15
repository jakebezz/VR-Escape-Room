using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton class for Sounds, used to make all sounds controlled through one audio source
    public static SoundManager Instance;

    AudioSource audioSource;

    //Sets Instance to Instance if it does not exist, detroys it if it already exists
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, bool loop)
    {
        audioSource.PlayOneShot(clip);
        audioSource.loop = loop;
    }

    public void PlaySoundAtPoint(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
