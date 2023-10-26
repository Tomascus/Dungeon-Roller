using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    //From N. Collins class
    //Fire sound from: https://www.freesoundeffects.com/free-sounds/fireball-10079/
    //Background music from: https://www.chosic.com/download-audio/39327/
    public AudioClip attackSound;
    public AudioClip backgroundMusic;
    private AudioSource soundEffectSource;
    private AudioSource backgroundMusicSource;
    public static AudioManager instance;
    
    // Start is called before the first frame update
    void Awake()
    {
      if(instance == null) 
      {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else{
        Destroy(gameObject);
        return;
      }

      soundEffectSource = gameObject.AddComponent<AudioSource>();
      backgroundMusicSource = gameObject.AddComponent<AudioSource>();

      backgroundMusicSource.clip = backgroundMusic;
      backgroundMusicSource.loop = true;
      backgroundMusicSource.Play();
      
    }



     public void PlayAttackSound()
    {
        soundEffectSource.PlayOneShot(attackSound);
        
    }

    public void PlayBackgroundMusic()
    {
        if(!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = 0.2f;
    }

}
