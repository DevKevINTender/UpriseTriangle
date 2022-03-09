using System.Collections;
using Controlers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeSessionCore : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private SessionPanelView sessionPanelView;
    [SerializeField] private PTSpawnBlockControler spawnBlockControler;
    [SerializeField] private AudioController audioController; // музыка уровня
    [SerializeField] private PTMovePointComponent movePointController;
    [SerializeField] private PTPersonComponent pTPersonComponent;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private SerciceScreenResolution serciceScreenResolution;
    [SerializeField] private SessionUIController sessionUIController;
    [SerializeField] private AttempCounter attempCounter;

    [Header("Game values")]
    [SerializeField] float musicTimeStart;
    [SerializeField] private int currentSession;
    [SerializeField] private float timeSlow;
    [SerializeField] private float gameSpeed;

    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // время старта игры
    private bool personWin;

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void SetGameSpeed()
    {
        gameSpeed = serciceScreenResolution.GetScaledGameSpeed();
    }

    void Start()
    {
        SetGameSpeed();
        sessionUIController.ActiveAttempText(attempCounter.GetAttemps());
        audioController.Play(musicTimeStart);
        pTPersonComponent.SetCanMove(true); 
        pTPersonComponent.InitComponent(PersonDeath, PersonWin, PersonEndWin); // подписка на событие смерти и выйгрыше игрока
        sessionPanelView.Init(StartPause, EndPause);// подписка на событие паузы
        spawnBlockControler.Init(); // загрузка уровня
        if (timeTransfer != 0)
        {
            audioController.TimeTransfer(timeTransfer); // старт музыки с заданного времени
            movePointController.TimeTransfer(timeTransfer, gameSpeed);
        }
    }
    public void RestartGame()
    {
        StartCoroutine(RestartTimer(animationController.GetPersonDeathAnimLength(), currentSession));
    }

    public void PersonDeath()
    {
        attempCounter.AddAttemp();
        animationController.PersonDeath();
        audioController.PersonDeath();
        pTPersonComponent.SetCanMove(false);
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
