using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionCore : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private AudioSource Music;
    [SerializeField] private GameObject ControlerPanel;

    [SerializeField] private SpawnBlockControler SpawnBlockControler;
    public int restartSessionNum;
    private float timer;
    public Text time;

    private bool isPause;
    public bool isStart;

    public float TimeToMusic;
    private float musicVolume;

    private IEnumerator startCoroutine; // переменна€ дл€ остановки ожидани€ старта

    public void OnApplicationPause(bool pause)
    {
        StartPause();
    }

    void Start()
    {
        musicVolume = Music.volume;
        Time.timeScale = 1;
        StartCoroutine(WaitToStartMusic(TimeToMusic));
        startCoroutine = StartSessionCur();
        StartCoroutine(startCoroutine);
       
        if(SpawnBlockControler) SpawnBlockControler.InitControler(0);        
        isStart = true;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        time.text = timer.ToString("F2");
    }

    public void LoseSession(float _time)
    {
        StartCoroutine(LooseSessionCur(_time));    
    }

    public void StopPause()
    {
        if (isPause)
        {
            ControlerPanel.transform.GetComponent<Image>().color = new Color32(26, 27, 33, 0);
            playerAnimator.SetBool("IsPause", false);
            Animator.SetBool("Pause", false);
            Time.timeScale = 1;
            Music.pitch = 1;
            Music.volume = musicVolume;
            isPause = false;
        }
    }

    public void StartPause()
    {
        if (isStart)
        {
            ControlerPanel.transform.GetComponent<Image>().color = new Color32(26, 27, 33, 200);
            
            playerAnimator.SetBool("IsPause", true);
            Animator.SetBool("Pause", true);
            
            Music.pitch = 0.1f;
            Music.volume = 0;
            
            Time.timeScale = 0.1f;
            isPause = true;
        }
    }    

    // таймер смерти игрока
    public IEnumerator LooseSessionCur(float _time)
    {
        ControlerPanel.SetActive(false);
        Music.volume = 0;
        Time.timeScale = 0.1f;
        Handheld.Vibrate();
        yield return new WaitForSecondsRealtime(_time);
        Time.timeScale = 1;
        SceneManager.LoadScene(restartSessionNum);
    }

    // таймер дл€ ожидани€ конца анимации старта или еЄ прерывани€
    public IEnumerator StartSessionCur()
    {
        float timer = 2f;
        while (timer > 0)
        {
            if (Animator.GetBool("Pause"))
                EmergencyStop();
            timer -= Time.deltaTime;
            yield return null;
        }
        Animator.SetBool("StartGame", true);
        StartCoroutine(WaitAnimationStartEnd());
    }

    private IEnumerator WaitAnimationStartEnd()
    {
        while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            yield return null;
        }
        StartPanel.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        isStart = true;
    }

    //врем€ начала музыки
    public IEnumerator WaitToStartMusic(float _time)
    {
        yield return new WaitForSecondsRealtime(_time);
        Music.Play();
    }


    public void EmergencyStop()
    {
        StopCoroutine(startCoroutine);
        Animator.SetBool("StartGame", true);
        StartCoroutine(WaitAnimationStartEnd());
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
