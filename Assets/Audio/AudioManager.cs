using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource runningSource;

    public AudioClip backgroundMusic;
    public AudioClip swordAttack;
    public AudioClip running;

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayRunning()
    {
        if (!runningSource.isPlaying)
        {
            runningSource.Play();
        }
    }

    public void StopRunning()
    {
        runningSource.Stop();
    }
}
