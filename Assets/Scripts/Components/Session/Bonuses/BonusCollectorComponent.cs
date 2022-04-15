using System;
using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;
using Views.Session.Bonuses;

public class BonusCollectorComponent : MonoBehaviour
{
    public delegate void StateGameDel();

    public StateGameDel startPause;
    public StateGameDel endPause;

    public Transform personObj;
    [Header("Objects")]
    public MCoinComponent MultiplierComponentObj;
    public MagnetComponent MagnetComponentObj;
    public ShieldComponent ShieldComponentObj;
    [Header("Prefabs")]
    public MCoinComponent MultiplierComponentPb;
    public MagnetComponent MagnetComponentPb;
    public ShieldComponent ShieldComponentPb;
    [Header("Panels")]
    public MagnetBonusPanelView MagnetBonusPanelObj;
    public ShieldBonusPanelView ShieldBonusPanelObj;
    public CoinBonusPanelView CoinBonusPanelObj;
    public MultiplierBonusPanelView MultiplierBonusPanelObj;
    [Header("Counter")]
    public int ShieldBonusCount;
    public int MagnetBonusCount;
    public int MultiplierBonusCount;
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

    public void InitComponent(StateGameDel startPause, StateGameDel endPause)
    {
        this.startPause = startPause;
        this.endPause = endPause;
   
        LoadBonusesCount();
        InitBonusComponents();
    }
    public void SubstractBonus(int id, int count) // отнять бонус
    {
        switch (id)
        {
            case 0: // бонусные монетки
            {
                CoinsControler.DecreaseCoins(count);
                break;
            }
            case 1: // щиток
            {
                ShieldBonusCount -= count;
                if (ShieldBonusCount <= 0)
                {
                    ShieldComponentObj.DeInitComponent();
                    Destroy(ShieldComponentObj);
                }
                SaveBonusesCount();
                break;
            }
            case 2: // магнит
            {
                MagnetBonusCount -= count;
                if (MagnetBonusCount <= 0)
                {
                    Destroy(MagnetComponentObj);
                }
                SaveBonusesCount();
                break;
            }
            case 3: // множитель монет
            {
                MultiplierBonusCount -= count;
                if (MultiplierBonusCount <= 0)
                {
                    Destroy(MultiplierComponentObj);
                }
                SaveBonusesCount();
                break;
            }
        }
    }
    public void AddBonus(int id, int count) // добавить бонус
    {
        switch (id)
        {
            case 0: // бонусные монетки
            {
                CoinsControler.UpcreaseCoins(count);
                break;
            }
            case 1: // щиток
            {
                if (ShieldBonusCount == 0)
                {
                    ShieldComponentObj = Instantiate(ShieldComponentPb, personObj);
                    ShieldComponentObj.InitComponent(SubstractBonus);
                }
                if(ShieldBonusCount < 99) ShieldBonusCount += count;
                SaveBonusesCount();
                break;
            }
            case 2: // магнит
            {
                if(MagnetBonusCount == 0) MagnetComponentObj = Instantiate(MagnetComponentPb, personObj);
                if(MagnetBonusCount < 99) MagnetBonusCount += count;
                SaveBonusesCount();
                break;
            }
            case 3: // множитель монет
            {
                if(MultiplierBonusCount == 0) MultiplierComponentObj = Instantiate(MultiplierComponentPb, personObj);
                if(MultiplierBonusCount < 99) MultiplierBonusCount += count;
                SaveBonusesCount();
                break;
            }
        } 
        EndPause();
    }
    
    public void PersonDeath()
    {
        if (ShieldBonusCount > 0)
        {
            SubstractBonus(1,1);
        }
        else
        {
            if (MagnetBonusCount > 0) SubstractBonus(2,1);
            if (MultiplierBonusCount > 0) SubstractBonus(3,1);
        }
        SaveBonusesCount();
    }

    private void InitBonusComponents()
    {
        if (ShieldBonusCount > 0)
        {
            ShieldComponentObj = Instantiate(ShieldComponentPb, personObj);
            ShieldComponentObj.InitComponent(SubstractBonus);
        }

        if (MagnetBonusCount > 0)
        {
            MagnetComponentObj = Instantiate(MagnetComponentPb, personObj);
        }

        if (MultiplierBonusCount > 0)
        {
            MultiplierComponentObj = Instantiate(MultiplierComponentPb, personObj);
        }
    }
    private void LoadBonusesCount()
    {
        ShieldBonusCount = PlayerPrefs.GetInt("ShieldBonusCount");
        MagnetBonusCount = PlayerPrefs.GetInt("MagnetBonusCount");
        MultiplierBonusCount = PlayerPrefs.GetInt("MultiplierBonusCount");
    }

    private void SaveBonusesCount()
    {
        PlayerPrefs.SetInt("ShieldBonusCount", ShieldBonusCount);
        PlayerPrefs.SetInt("MagnetBonusCount", MagnetBonusCount);
        PlayerPrefs.SetInt("MultiplierBonusCount", MultiplierBonusCount);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MagnetBonusComponent>())
        {
            MagnetBonusPanelObj.gameObject.SetActive(true);
            MagnetBonusPanelObj.InitView(AddBonus, ClosePanel);
            StartPause();
        }
        if (other.GetComponent<ShieldBonusComponent>())
        {
            ShieldBonusPanelObj.gameObject.SetActive(true);
            ShieldBonusPanelObj.InitView(AddBonus, ClosePanel);
            StartPause();
        }
        if (other.GetComponent<MultiplierBonusComponent>())
        {
            MultiplierBonusPanelObj.gameObject.SetActive(true);
            MultiplierBonusPanelObj.InitView(AddBonus, ClosePanel);
            StartPause();
        }
        if (other.GetComponent<CoinBonusComponent>())
        {
            CoinBonusPanelObj.gameObject.SetActive(true);
            CoinBonusPanelObj.InitView(AddBonus, ClosePanel);
            StartPause();
        }
    }

    private void CheckEnothBonusCount() // уничтожение объектов бонуса с игрока, если нет достаточно колличества бонусов
    {
        if (ShieldBonusCount <= 0)
        {
            Destroy(ShieldComponentObj);
        }
        if (MagnetBonusCount <= 0)
        {
            Destroy(MagnetComponentObj);
        }
        if (MultiplierBonusCount <= 0)
        {
            Destroy(MagnetComponentObj);
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

    private void ClosePanel() // закрытие панели бонусов
    {
        EndPause();
    }
}