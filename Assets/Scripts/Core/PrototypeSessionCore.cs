using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class PrototypeSessionCore : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private PTSpawnBlockControler SpawnBlockControler;
    [SerializeField] private AudioController audioController; // ������ ������
    [SerializeField] private PTMovePointComponent movePoint;
    [SerializeField] private PTPersonComponent pTPersonComponent;
    [Header("Game values")]
    [SerializeField] private float timeSlow;
    [SerializeField] private float gameSpeed;
    [Header("Player transfer")]
    [SerializeField] private float timeTransfer; // ����� ������ ����

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    void Start()
    {
        pTPersonComponent.InitComponent(PersonDeath);
        SpawnBlockControler.Init(); // �������� ������
        if (timeTransfer != 0)
        {
            audioController.TimeTransfer(timeTransfer); // ����� ������ � ��������� �������
            movePoint.TimeTransfer(timeTransfer, gameSpeed);
        }
    }

    public void PersonDeath()
    {

    }


    public void StartPause()
    {
        audioController.StartPause(timeSlow);
        Time.timeScale = timeSlow;
    }

    public void OnApplicationPause()
    {
        StartPause();
    }
}
