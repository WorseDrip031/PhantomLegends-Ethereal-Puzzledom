using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource runningSource;
    [SerializeField] AudioSource enemySource;
    [SerializeField] AudioSource itemSource;
    [SerializeField] AudioSource levelUpSource;

    public AudioClip backgroundMusic;
    public AudioClip swordAttack;
    public AudioClip running;
    public AudioClip doorOpening;
    public AudioClip goblinDeath;
    public AudioClip itemPickup;
    public AudioClip levelUp;

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == goblinDeath)
        {
            enemySource.PlayOneShot(clip);
        }
        else if (clip == itemPickup)
        {
            itemSource.PlayOneShot(clip);
        }
        else if (clip == levelUp)
        {
            levelUpSource.PlayOneShot(clip);
        }
        else
        {
            SFXSource.PlayOneShot(clip);
        }
        Debug.Log(clip.name);
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
