using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionUIController : MonoBehaviour
{
    [SerializeField] private Text curTime;
    [SerializeField] private Text attempText;
    [SerializeField] private GameObject winPanel;

    private float curTimer;


    public void ActiveAttempText(int attemps)
    {
        attempText.text = "Attemp " + attemps;
        attempText.gameObject.SetActive(true);
    }

    public void ActivateWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void UpdateCoinsText(int _coins)
    {
        
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
