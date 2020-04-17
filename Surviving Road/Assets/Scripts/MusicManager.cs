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

    public void SwitchMusic(string theme)
    {
        AudioClip tempoClip;
        switch(theme)
        {
            case "main":
                tempoClip = mainTheme;
                break;
            case "battle":
                tempoClip = battleTheme;
                break;
            default:
                tempoClip = null;
                break;
        }
        if(audioSource.clip == tempoClip) PlayMusic();
    }
}
