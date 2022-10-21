using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    public AudioSource audioSourceFirstMusc;
    public AudioSource audioSourceSecMusc;
    public AudioSource effectsAudioSource;
    public AudioClip dogJumpAudClip, dogBendOverAudClip, dogMoveToRightLeftAudClip, dogDeathAudClip;
    public AudioClip goldCollectedAudClip;
    public float audioVolume;

    void Start()
    {
        Invoke("PlaySecondMusic", 63);

        SetTheVolume();
    }

    private void SetTheVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume")) 
        {
            Debug.Log("Music Volume = " + PlayerPrefs.GetFloat("musicVolume"));
            audioVolume = PlayerPrefs.GetFloat("musicVolume");
            audioSourceFirstMusc.volume = audioVolume;
            audioSourceSecMusc.volume = audioVolume;
            effectsAudioSource.volume = audioVolume;
        }
        else
        {
            Debug.Log("Music Volume Not Found");
            Debug.Log("Music Volume = " + PlayerPrefs.GetFloat("musicVolume"));
            audioVolume = 1;
            audioSourceFirstMusc.volume = audioVolume;
            audioSourceSecMusc.volume = audioVolume;
            effectsAudioSource.volume = audioVolume;
        }

    }

    private void PlaySecondMusic()
    {
        audioSourceSecMusc.Play();
    }

    public void DogJumpedSoundFunc()
    {
        effectsAudioSource.PlayOneShot(dogJumpAudClip);
    }
    public void DogBendOverSoundFunc()
    {
        effectsAudioSource.PlayOneShot(dogBendOverAudClip);
    }
    public void DogMoveToRightLeftSoundFunc()
    {
        effectsAudioSource.PlayOneShot(dogMoveToRightLeftAudClip);
    }
    public void DogDeathSoundFunc()
    {
        effectsAudioSource.PlayOneShot(dogDeathAudClip);
    }
    public void GoldCollectedSoundFunc()
    {
        effectsAudioSource.PlayOneShot(goldCollectedAudClip);
    }
    

}
