using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controlers;
using UnityEngine;

public class BonusControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPointList = new List<GameObject>();
    [SerializeField] private GameObject MultiplierBonusPb;
    [SerializeField] private GameObject MagnetBonusPb;
    [SerializeField] private GameObject ShieldBonusPb;
    [SerializeField] private GameObject CoinBonusPb;

    [SerializeField] private GameObject MagnetBonusPanelView;
    
    void Start()
    {
        //CheckDRList();
    }
    
    public void SpawnBonus(int checkPointId, GameObject typeBonus)
    {
        Instantiate(typeBonus, checkPointList[checkPointId].transform);
    }

    
    // тестовая функция проверки
    private void CheckDRList()
    {
        List<DeathRecord> deathRecordList = DeathRegistrationControler.GetRecordList().List;
        int countFit = 0;
        DateTime lastTime;
        int lastCheckPoint = deathRecordList.Last().checkPoint;

        TimeSpan rez = DateTime.UtcNow - DateTime.FromFileTimeUtc(deathRecordList.Last().time);
        Debug.Log(DateTime.FromFileTimeUtc(deathRecordList.Last().time));
        Debug.Log(rez.TotalMinutes);
        if (rez.TotalMinutes < 5)
        {
            for (int i = deathRecordList.Count-1; i >= 0; i--)
            {
                deathRecordList[i].checkPoint = lastCheckPoint;
                countFit++;
            }
        }

        if (countFit >= 3)
        {
            SpawnBonus(lastCheckPoint, MultiplierBonusPb);
        }

    }
}
