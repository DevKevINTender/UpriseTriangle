using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonusComponent : MonoBehaviour
{
    public int shieldPointsCount;

    public void InitComponent()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckExitShiled())
        {
            CreateNewShieldComponent();
            AddPointToShieldComponent();
        }
        else
        {
            AddPointToShieldComponent();
        }
    }

    bool CheckExitShiled()
    {
        return true;
    }

    void AddPointToShieldComponent()
    {
        
    }

    void CreateNewShieldComponent()
    {
        
    }
}
