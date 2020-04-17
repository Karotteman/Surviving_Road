using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SwitchMusic()
    {
        if (audioSource.clip == mainTheme)
        {
            audioSource.clip = battleTheme;
        }
        else
        {
            audioSource.clip = mainTheme;
        }
        PlayMusic();
    }
}
