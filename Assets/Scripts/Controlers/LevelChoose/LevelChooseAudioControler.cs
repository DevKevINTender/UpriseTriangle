using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class LevelChooseAudioControler : MonoBehaviour
{
    [SerializeField] private AudioSource currentLevelMusic;
    // Start is called before the first frame update
    void Start()
    {
        int id = LevelChooseControler.GetCurrentLevel();
        currentLevelMusic.clip = LevelChooseControler.GetLevelById(id).musicAudio;
        currentLevelMusic.Play();

    }

    public void SetCurrentLevelMusic(int id)
    {
        currentLevelMusic.clip = LevelChooseControler.GetLevelById(id).musicAudio;
        currentLevelMusic.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
