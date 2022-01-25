using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionUIController : MonoBehaviour
{
    [SerializeField] private Text curTime;
    private float curTimer;


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
    }
}
