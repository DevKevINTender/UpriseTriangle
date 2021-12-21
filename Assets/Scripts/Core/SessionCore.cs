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
    void Start()
    {
        StartCoroutine(StartSessionCur());
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
        float timerAnim = Animator.runtimeAnimatorController.animationClips[1].length;
        float timer = 2.5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Animator.SetBool("StartGame", true);
        StartCoroutine(WaitAnimationStartEnd(timerAnim));
       
    }
    private IEnumerator WaitAnimationStartEnd(float _time)
    {
        yield return new WaitForSeconds(_time);
        StartPanel.SetActive(false);
        isStart = true;
    }
    
    
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
