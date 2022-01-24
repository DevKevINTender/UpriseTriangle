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
    [Header("Game values")]
    [SerializeField] float musicTimeStart;
    [SerializeField] private int currentSession;
    [SerializeField] private float timeSlow;
    [SerializeField] private float gameSpeed;
    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // время старта игры

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    void Start()
    {
        audioController.Play(musicTimeStart);
        pTPersonComponent.SetCanMove(true); 
        pTPersonComponent.InitComponent(PersonDeath); // подписка на событие смерти игрока
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
        animationController.PersonDeath();
        audioController.PersonDeath();
        pTPersonComponent.SetCanMove(false);
        Handheld.Vibrate();
        Time.timeScale = timeSlow;
        RestartGame();
    }

    public void StartPause()
    {
        animationController.StartPause();
        audioController.StartPause(timeSlow);
        Time.timeScale = timeSlow;
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
