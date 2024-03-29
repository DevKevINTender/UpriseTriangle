using System.Collections;
using System.Collections.Generic;
using Controlers;
using Controlers.Settings;
using UnityEngine;

public class SessionAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;

    [SerializeField] private float musicVolume;
    void Awake()
    {
        
    }
    public void Play(float _delay)
    {
        audiosource.volume = 1 * (SettingsControler.GetSessionMusicVolume() / 10f);
        musicVolume =  SettingsControler.GetSessionMusicVolume() / 10f;
        StartCoroutine(WaitToStartMusic(_delay));
    }

    // старт с заданного времени
    public void TimeTransfer(float _time)
    {
        audiosource.time = _time;
    }

    public void StartPause(float _timeSlow)
    {
        audiosource.pitch = _timeSlow;
        audiosource.volume = 0;
    }


    public void EndPause()
    {
        audiosource.pitch = 1;
        audiosource.volume = musicVolume;
    }

    public void PersonDeath()
    {
        audiosource.volume = 0;
    }

    public IEnumerator WaitToStartMusic(float _delay)
    {
        int id = LevelChooseControler.GetCurrentLevel();
        float audioDelay = LevelChooseControler.GetLevelById(id).audioDelay;
        
        yield return new WaitForSecondsRealtime(_delay + audioDelay);
        
        audiosource.clip = LevelChooseControler.GetLevelById(id).musicAudio;
        audiosource.Play();
    }
}
