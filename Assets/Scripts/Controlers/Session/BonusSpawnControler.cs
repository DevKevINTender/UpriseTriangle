using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controlers;
using UnityEngine;

public class BonusSpawnControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPointList = new List<GameObject>();
    [SerializeField] private List<GameObject> bonusesList = new List<GameObject>();

    void Start()
    {
        RandomSpawnBonuses();
        //CheckDRList();
    }
    
    public void SpawnBonus(int checkPointId, int id)
    {
       
    }

    public void RandomSpawnBonuses()
    {
        for (int i = 0; i < checkPointList.Count; i++)
        {
            int id = UnityEngine.Random.Range(0, bonusesList.Count);
            Instantiate(bonusesList[id], checkPointList[i].transform);
        }
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
            //SpawnBonus(lastCheckPoint, MultiplierBonusPb);
        }

    }
}
