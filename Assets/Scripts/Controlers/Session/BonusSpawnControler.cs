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
    [SerializeField] private float coinPersent;
    [SerializeField] private float multiPercent;
    [SerializeField] private float magnetPercent;
    void Start()
    {
        SpawnShieldBonus();
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

    public void SpawnShieldBonus()
    {
        List<GameObject> freeCheckPointList = new List<GameObject>();
        freeCheckPointList = GetFreeCheckPoint();
        Transform checkPoint = freeCheckPointList[UnityEngine.Random.Range(0, freeCheckPointList.Count)].transform;
        Instantiate(bonusesList[0], checkPoint);
    }

    public List<GameObject> GetFreeCheckPoint()
    {
        List<GameObject> freeCheckPoint = new List<GameObject>();
        foreach (var item in checkPointList)
        {
            if (item.transform.GetChildCount() == 0)
            {
                freeCheckPoint.Add(item);
            }
        }

        return freeCheckPoint;
    }
}
