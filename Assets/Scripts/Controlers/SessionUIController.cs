using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionUIController : MonoBehaviour
{

    [SerializeField] private Text coinsText;
    [SerializeField] private Text curPercentText;

    void Start()
    {
        
    }

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
}
