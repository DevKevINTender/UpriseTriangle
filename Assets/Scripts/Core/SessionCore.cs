using System.Collections;
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
    [SerializeField] private SerciceScreenResolution serciceScreenResolution;
    [SerializeField] private SessionUIController sessionUIController;
    [SerializeField] private AttempCounterController attempCounterController;

    [Header("Game values")]
    [SerializeField] float musicTimeStart;
    [SerializeField] private int currentSession;
    [SerializeField] private float timeSlow;
    [SerializeField] private float gameSpeed;

    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // ����� ������ ����
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
        sessionUIController.ActiveAttempText(attempCounterController.GetAttemps());
        audioController.Play(musicTimeStart);
        pTPersonComponent.SetCanMove(true); 
        pTPersonComponent.InitComponent(PersonDeath, PersonWin, PersonEndWin); // �������� �� ������� ������ � �������� ������
        playerMovePanelView.Init(StartPause, EndPause);// �������� �� ������� �����
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
