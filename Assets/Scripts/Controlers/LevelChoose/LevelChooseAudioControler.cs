using System;
using System.Collections;
using System.Collections.Generic;
using Controlers;
using Controlers.Settings;
using DG.Tweening;
using UnityEngine;

public class LevelChooseAudioControler : MonoBehaviour
{
    [SerializeField] private AudioSource currentLevelMusic;
    
    public Sequence MusicPanelSeq;

    public float currentVolume;
    // Start is called before the first frame update
    void Awake()
    {
        int id = LevelChooseControler.GetCurrentLevel();
        currentLevelMusic.clip = LevelChooseControler.GetLevelById(id).musicAudio;
        currentLevelMusic.time = PlayerPrefs.GetFloat("CurrentTimeMusic");
        currentLevelMusic.Play();
       

    }
    public void StopMusic()
    {
        MusicPanelSeq.Pause();
        MusicPanelSeq.Kill();
        PlayerPrefs.SetFloat("CurrentTimeMusic",currentLevelMusic.time);
    }

    public void SetCurrentValume(float volume)
    {
        currentVolume = volume;
        currentLevelMusic.volume = currentVolume;
    }
    public void SetCurrentLevelMusic(int id)
    {
        MusicPanelSeq.Kill();
        MusicPanelSeq = DOTween.Sequence();
        currentLevelMusic.clip = LevelChooseControler.GetLevelById(id).musicAudio;
        currentLevelMusic.time = 0; 
        currentLevelMusic.volume = 0;
        MusicPanelSeq.Append(currentLevelMusic.DOFade(SettingsControler.GetMenuMusicVolume(), 5f));
        currentLevelMusic.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
