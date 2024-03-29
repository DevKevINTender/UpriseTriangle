using System;
using System.Collections;
using Services;
using Controlers;
using Controlers.Session;
using DG.Tweening;
using DOTweenAnimation.Global;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class SessionCore : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private PlayerMovePanelView playerMovePanelView;
    [SerializeField] private SessionAudioController audioController; // ������ ������
    [SerializeField] private MovePointComponent movePointController;
    [SerializeField] private PersonComponent personComponent;
    [SerializeField] private SessionAnimationController animationController;
    [SerializeField] private SessionUIController sessionUIController;
    [SerializeField] private LevelStatsCounterController levelStatsCounterController;
    [SerializeField] private WinnerPanelControler winnerPanelControler;
    [SerializeField] private BonusCollectorComponent bonusCollectorComponent;
    [SerializeField] private BonusSpawnControler bonusSpawnControler;

    [Header("Game values")]
    [SerializeField] float musicTimeStart;
    [SerializeField] private int currentSession;
    [SerializeField] private float timeSlow;
    private float gameSpeed;

    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // ����� ������ ����
    private bool personWin;
    private bool personDeath;
    
    [Header("Animations")]
    [SerializeField] private PausePanelAnimation PausePanelAnimation;
    [SerializeField] private TransitionAnimation TransitionPanelAnimation;
    [SerializeField] private WinnerPanelAnimationController WinnerPanelAnimation;

    public void SetGameSpeed()
    {
        gameSpeed = ServiceScreenResolution.GetScaledGameSpeed();
        Time.timeScale = 1;
    }

    void Start()
    {
        //Appodeal.setTesting(true);
        Appodeal.initialize("b80f8019ac0cc44cfbb1e31c31c7e59f13dc2a850a6ab8e4", Appodeal.REWARDED_VIDEO, true);
        Appodeal.cache(Appodeal.REWARDED_VIDEO);
        
        currentSession = LevelChooseControler.GetCurrentLevel();
        
        SetGameSpeed();
        LoadSessionLevel();
        DOTween.Init();
        sessionUIController.ActiveAttempText(levelStatsCounterController.GetAttemps());
        levelStatsCounterController.InitControler(currentSession);
        bonusSpawnControler.InitControler();
        audioController.Play(musicTimeStart);
        TransitionPanelAnimation.gameObject.SetActive(true);
        TransitionPanelAnimation.OpenSessionScene();
        personComponent.SetCanMove(true); 
        
        personComponent.InitComponent(PersonDeath, PersonWin, PersonEnterElevator, PersonExitElevator); // �������� �� ������� ������ � �������� ������
        playerMovePanelView.Init(StartPause, EndPause);// �������� �� ������� �����
        bonusCollectorComponent.InitComponent(StartPause, EndPause);
        if (timeTransfer != 0)
        {
            audioController.TimeTransfer(timeTransfer); // ����� ������ � ��������� �������
            movePointController.TimeTransfer(timeTransfer, gameSpeed);
        }        
    }

    private void LoadSessionLevel()
    {
        SessionLevelScrObj level = LevelChooseControler.GetLevelById(LevelChooseControler.GetCurrentLevel());
        Instantiate(level.SessionLevelPB, transform.position, Quaternion.identity);
    }
    public void PersonDeath()
    {
        personDeath = true;
        levelStatsCounterController.ChangeLevelStats();
        //animationController.PersonDeath();
        audioController.PersonDeath();
        bonusCollectorComponent.PersonDeath();
        personComponent.SetCanMove(false);
        //DeathRegistrationControler.AddNewRecord(DateTime.Now,1);

        Handheld.Vibrate();
        Time.timeScale = timeSlow;
        
        TransitionPanelAnimation.CloseSessionScene(0, "Session");
    }

    public void PersonEnterElevator()
    {
        animationController.PersonEnterElevator();
    }

    public void PersonExitElevator()
    {
        animationController.PersoExitElevator();
    }

    public void PersonWin()
    {
        personWin = true;
        personComponent.SetCanMove(false);
        Time.timeScale = timeSlow;
        WinnerPanelAnimation.gameObject.SetActive(true);
        WinnerPanelAnimation.PersonWin();
        winnerPanelControler.InitControler(currentSession);
    }

    public void StartPause()
    {
        if (!personWin && !personDeath)
        { 
            //animationController.StartPause();
            PausePanelAnimation.OpenPanelAnim();
            audioController.StartPause(timeSlow);
            Time.timeScale = timeSlow;
        }
    }

    public void EndPause()
    {
        //animationController.EndPause();
        audioController.EndPause();
        PausePanelAnimation.ClosePanelAnim(0);
        Time.timeScale = 1f;
    }

    public void OnApplicationPause()
    {
        #if !UNITY_EDITOR
        StartPause();
        #endif
    }
}
