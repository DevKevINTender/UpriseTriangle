using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionUIController : MonoBehaviour
{
    [SerializeField] private Text curTime;
    [SerializeField] private Text curfps;
    [SerializeField] private Text curDeltaTime;
    [SerializeField] private Text curFixedDeltaTime;
    private float curTimer;
    private float fps;


    [SerializeField] private Text coinsText;
    [SerializeField] private Text curPercentText;

    public void UpdateCoinsText(int _coins)
    {
        coinsText.text = _coins.ToString();
    }

    public void UpdateCurrentPersent(int _curPercent)
    {
        curPercentText.text = _curPercent.ToString();
    }


    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }



    public void Update()
    {
        curTimer += Time.deltaTime;
        curTime.text = curTimer.ToString("F2");
        fps = 1 / Time.unscaledDeltaTime;
        curfps.text = fps.ToString("F2");
        curDeltaTime.text = Time.deltaTime.ToString("F3");
        curFixedDeltaTime.text = Time.fixedDeltaTime.ToString("F3");
    }
}
