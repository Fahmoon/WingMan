using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioClip[] sound;
    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
//        Debug.Log("here");
        effectsSource.clip = clip;
        effectsSource.Play();
    }
    public void Stop()
    {

        effectsSource.Stop();
    }
    public void Mute()
    {
        effectsSource.volume = 0f;
    }
    public void notMute()
    {
        effectsSource.volume = 8f;
    }

  
}
