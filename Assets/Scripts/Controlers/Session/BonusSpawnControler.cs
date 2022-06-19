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
    [SerializeField] private GameObject shieldBonus;
    [SerializeField] private float coinPersent;
    [SerializeField] private float multiPercent;
    [SerializeField] private float magnetPercent;

    public void InitControler()
    {
        CheckPointComponent[] list = FindObjectsOfType<CheckPointComponent>();
        foreach (var item  in list)
        {
            checkPointList.Add(item.gameObject.transform.GetChild(2).gameObject);
        }
      
        SpawnShieldBonus();
        SpawnAnotherBonuses();
    }
    private void SpawnAnotherBonuses()
    {
        List<GameObject> freeCheckPointList = new List<GameObject>();
        freeCheckPointList = GetFreeCheckPoint();

        foreach (var item in freeCheckPointList)
        {
            int id = UnityEngine.Random.Range(0, bonusesList.Count);
            Instantiate(bonusesList[id], item.transform);
        }
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
        Instantiate(shieldBonus, checkPoint);
    }

    public List<GameObject> GetFreeCheckPoint()
    {
        List<GameObject> freeCheckPoint = new List<GameObject>();
        foreach (var item in checkPointList)
        {
            if (item.transform.childCount == 0)
            {
                freeCheckPoint.Add(item);
            }
        }

        return freeCheckPoint;
    }
}
