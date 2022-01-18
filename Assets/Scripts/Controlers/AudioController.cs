using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;
    void Start()
    {
        
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


    public void StopPause(float _musicVolume)
    {
        audiosource.pitch = 1;
        audiosource.volume = _musicVolume;
    }
}
