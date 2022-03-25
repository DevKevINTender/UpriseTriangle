using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCollectorComponent : MonoBehaviour
{
    public delegate void PauseDel();

    public PauseDel startPause;
    public PauseDel endPause;

    public Transform personObj;
    public MCoinComponent multiplierComponentPb;
    public MagnetComponent magnetComponentPb;
    public ShieldComponent ShieldComponentPb;

    public MagnetBonusPanelView magnetBonusPanelPb;
    
    public int ShieldBonusCount;
    public int MagnetBonusCount;
    public int MultiplierBonusCount;

    public void InitComponent(PauseDel startPause, PauseDel endPause)
    {
        this.startPause = startPause;
        this.endPause = endPause;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MagnetBonusComponent>())
        {
            magnetBonusPanelPb.gameObject.SetActive(true);
            magnetBonusPanelPb.InitView(GetBonus, ClosePanel);
            StartPause();
        }
    }
    public int GetShieldBonusCount()
    {
        return ShieldBonusCount;
    }
    
    public int GetMagnetBonusCount()
    {
        return MagnetBonusCount;
    }
    
    public int GetMultiplierBonusCount()
    {
        return MultiplierBonusCount;
    }
    
    public void NewAttemp()
    {
        if (ShieldBonusCount > 0)
        {
            ShieldBonusCount--;
        }
        else
        {
            if (MagnetBonusCount > 0) MagnetBonusCount--;
            if (MultiplierBonusCount > 0) MultiplierBonusCount--;
        }
    }

    private void StartPause()
    {
        startPause?.Invoke();
    }

    private void EndPause()
    {
        endPause?.Invoke();
    }

    private void ClosePanel()
    {
        EndPause();
    }
    public void GetBonus(int id)
    {
        switch (id)
        {
            case 0: // бонусные монетки
            {
                break;
            }
            case 1: // щиток
            {
                ShieldBonusCount++;
                break;
            }
            case 2: // магнит
            {
                if(MagnetBonusCount == 0) Instantiate(magnetComponentPb, personObj);
                if(MagnetBonusCount < 5) MagnetBonusCount++;
                break;
            }
            case 3: // множитель монет
            {
                MultiplierBonusCount++;
                break;
            }
        } 
        EndPause();
    }
}
