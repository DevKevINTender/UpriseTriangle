using System;
using System.Collections;
using Services;
using Controlers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionCore : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private PlayerMovePanelView playerMovePanelView;
    [SerializeField] private SessionAudioController audioController; // ������ ������
    [SerializeField] private MovePointComponent movePointController;
    [SerializeField] private PersonComponent pTPersonComponent;
    [SerializeField] private SessionAnimationController animationController;
    [SerializeField] private SessionUIController sessionUIController;
    [SerializeField] private AttempCounterController attempCounterController;
    [SerializeField] private BonusCollectorComponent bonusCollectorComponent;
    [Header("Game values")]
    [SerializeField] float musicTimeStart;
    [SerializeField] private int currentSession;
    [SerializeField] private float timeSlow;
    private float gameSpeed;

    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // ����� ������ ����
    private bool personWin;

    public void SetGameSpeed()
    {
        gameSpeed = ServiceScreenResolution.GetScaledGameSpeed();
    }

    void Start()
    {
        SetGameSpeed();
        sessionUIController.ActiveAttempText(attempCounterController.GetAttemps());
        audioController.Play(musicTimeStart);
        pTPersonComponent.SetCanMove(true); 
        pTPersonComponent.InitComponent(PersonDeath, PersonWin, PersonEndWin); // �������� �� ������� ������ � �������� ������
        playerMovePanelView.Init(StartPause, EndPause);// �������� �� ������� �����
        //bonusCollectorComponent.InitComponent(StartPause, EndPause);
        if (timeTransfer != 0)
        {
            audioController.TimeTransfer(timeTransfer); // ����� ������ � ��������� �������
            movePointController.TimeTransfer(timeTransfer, gameSpeed);
        }
       
    }
    public void RestartGame()
    {
        StartCoroutine(RestartTimer(animationController.GetPersonDeathAnimLength(), currentSession));
    }

    public void PersonDeath()
    {
        attempCounterController.AddAttemp();
        animationController.PersonDeath();
        audioController.PersonDeath();
        pTPersonComponent.SetCanMove(false);
        DeathRegistrationControler.AddNewRecord(DateTime.Now,1);
        Handheld.Vibrate();
        Time.timeScale = timeSlow;
        RestartGame();
    }

    public void PersonEndWin()
    {
        sessionUIController.ActivateWinPanel();
    }

    public void PersonWin()
    {
        personWin = true;
        pTPersonComponent.SetCanMove(false);
        pTPersonComponent.MoveToCenter();
        animationController.PersonWin();      
    }

    public void StartPause()
    {
        if (!personWin)
        { 
            animationController.StartPause();
            audioController.StartPause(timeSlow);
            Time.timeScale = timeSlow;
        }
    }

    public void EndPause()
    {
        
        animationController.EndPause();
        audioController.EndPause();
        Time.timeScale = 1f;
    }

    public void OnApplicationPause()
    {
        #if !UNITY_EDITOR
        StartPause();
        #endif
    }

    public IEnumerator RestartTimer(float _time, int currentSession)
    {        
        yield return new WaitForSecondsRealtime(_time);
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSession);
    }
}
