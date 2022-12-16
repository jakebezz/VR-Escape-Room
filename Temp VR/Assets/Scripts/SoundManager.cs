using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class for Sounds, used to make some sound controlled through one audio source
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    /// <summary>
    /// Sets Instance to Instance if it does not exist, detroys it if it already exists
    /// </summary>
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

    /// <summary>
    /// Plays a sound at a set Location 
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlaySoundAtPoint(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
