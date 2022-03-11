using System;
using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class BonusControler : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkPointList = new List<GameObject>();
    [SerializeField] private GameObject coinBonusPb;
    [SerializeField] private GameObject jumperBonusPb;
    [SerializeField] private GameObject savePointBonusPb;
    [SerializeField] DeathRegistrationControler DeathRegistrationControler = new DeathRegistrationControler();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateBonuses()
    {
        List<DeathRecord> deathRecordList = DeathRegistrationControler.GetRecordList().List;
        int countFit = 0;
        DateTime lastTime;
        DateTime lastPercent;
        for (int i = deathRecordList.Count; i >=0; i--)
        {
            
        }
    }
}
