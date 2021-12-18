using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionCore : MonoBehaviour
{
    [SerializeField] private Animation SessionAnimation;
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
            SessionAnimation.Play("StopPause");
            SessionAnimation["StopPause"].speed = 1;
            Time.timeScale = 1;
            isPause = false;
        }
    }
    public void StartPause()
    {
        if (isStart)
        {
            SessionAnimation.Play("StartPause");
            SessionAnimation["StartPause"].speed = 10;
            Time.timeScale = 0.1f;
            isPause = true;
        }
    }

    public IEnumerator StartSessionCur()
    {
        float timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        SessionAnimation.Play("StopPause");
        isStart = true;
        if (!isPause)
        {
            
        }
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
