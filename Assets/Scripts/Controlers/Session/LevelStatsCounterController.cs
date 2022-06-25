using System;
using Controlers;
using ScriptableObjects.SessionLevel;
using UnityEngine;

public class LevelStatsCounterController : MonoBehaviour
{
    [SerializeField] private int currentId;
    private int attemp;
    private int coinCount;
    private float levelCompletePercent;
    [SerializeField] private float timer;
    [SerializeField] private int totalCoin;

    public void Update()
    {
        timer += Time.deltaTime;
    }

    public void InitControler(int id)
    {
        this.currentId = id;
        CoinsControler.IncreaseCoinsEvent += AddCoin;
    }

    public void OnDestroy()
    {
        CoinsControler.IncreaseCoinsEvent -= AddCoin;
    }

    public void ChangeLevelStats()
    {
        SaveAttemps();
        SaveCompletePercent(timer);
        SaveCoinsCollect(totalCoin);
    }

    public void AddCoin(int coinCount)
    {
        totalCoin += coinCount;
    }
    public void SaveCompletePercent(float currentTime)
    {
        SessionLevelScrObj level = LevelChooseControler.GetLevelById(LevelChooseControler.GetCurrentLevel());
        float percent = currentTime / level.MusicTime * 100;
        if (percent > level.CompletePercent)
        {
            LevelChooseControler.SetSessionCompletePercent(currentId, percent);
        }
    }
    
    public float LoadCompletePercent()
    {
        return LevelChooseControler.GetSessionCompletePercent(currentId);
    }
    
    public void SaveAttemps()
    {
        attemp = GetAttemps();
        attemp++;
        LevelChooseControler.SetSessionAttempCount(currentId, attemp);
    }

    public int GetAttemps()
    {
        return LevelChooseControler.GetSessionAttempCount(currentId);
    }
    
    public void SaveCoinsCollect(int newCoins)
    {
        LevelChooseControler.SetSessionCoinsCollectCount(currentId, newCoins);
    }

    public int LoadCoinsCollect()
    {
        return LevelChooseControler.GetSessionAttempCount(currentId);
    }
    
    public void ClearAttemps()
    {
        attemp = 1;
        LevelChooseControler.SetSessionAttempCount(currentId, attemp);
    }
}
