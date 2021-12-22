using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionCore : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject PausePanel;
    private bool isPause;
    public bool isStart;

    private IEnumerator startCoroutine;
    void Start()
    {
        Time.timeScale = 1;
        startCoroutine = StartSessionCur();
        StartCoroutine(startCoroutine);
        isStart = true;
    }
    
    void Update()
    {
        
    }

    public void LoseSession()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
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
