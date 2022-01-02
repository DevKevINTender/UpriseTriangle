using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionCore : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private AudioSource Music;
    [SerializeField] private GameObject ControlerPanel;

    [SerializeField] private SpawnBlockControler SpawnBlockControler;
    public int restartSessionNum;
    private float timer;
    public Text time;

    private bool isPause;
    public bool isStart;
    public float TimeToMusic; 
    

    private IEnumerator startCoroutine; // переменна€ дл€ остановки ожидани€ старта

    void Start()
    {
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

    public void LoseSession()
    {
        StartCoroutine(LooseSessionCur());
        
    }

    public void StopPause()
    {
        if (isPause)
        {
            Animator.SetBool("Pause", false);
            Animator.speed = 1;
            Time.timeScale = 1;
            isPause = false;
        }
    }
    public void StartPause()
    {
        if (isStart)
        {
            Animator.SetBool("Pause", true);
            Animator.speed = 10;
            Time.timeScale = 0.1f;
            isPause = true;
        }
    }    
    // таймер смерти игрока
    public IEnumerator LooseSessionCur()
    {
        ControlerPanel.SetActive(false);
        //Time.timeScale = 0.1f;
        float timer = 0.5f;
        while (Time.timeScale >= 0.1)
        {
            Time.timeScale -= Time.deltaTime * 2;
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(restartSessionNum);
    }
    public IEnumerator LooseSessionCurSec()
    {
        ControlerPanel.SetActive(false);
        Time.timeScale = 0.1f;
        float timer = 0.5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime * 2;
            yield return null;
        }
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
        StartCoroutine(WaitStart(1f));
    }

    private IEnumerator WaitStart(float _time)
    {
        yield return new WaitForSeconds(_time);
        isStart = true;
    }

    //врем€ начала музыки
    public IEnumerator WaitToStartMusic(float _time)
    {
        float timer = _time;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
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
