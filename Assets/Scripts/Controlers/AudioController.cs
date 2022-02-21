using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;

    private float musicVolume;
    void Awake()
    {
        musicVolume = audiosource.volume;
        Debug.Log("AudioAwake " + Time.fixedTime);
    }
    public void Play(float _delay)
    {
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
        yield return new WaitForSecondsRealtime(_delay);
        Debug.Log("AudioPlay " + Time.fixedTime );
        audiosource.Play();
    }
}
