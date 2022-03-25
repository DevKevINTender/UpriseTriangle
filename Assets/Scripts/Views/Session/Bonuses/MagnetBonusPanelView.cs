using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBonusPanelView : MonoBehaviour
{
    private int BonusId = 2;
    
    public delegate void GetBonusDel(int id);
    public delegate void ClosePanelDel();
    
    public GetBonusDel getBonus;
    public ClosePanelDel closePanel;
    public void InitView(GetBonusDel getBonus, ClosePanelDel closePanel)
    {
        this.getBonus = getBonus;
        this.closePanel = closePanel;
    }

    public void GetFreeBonus()
    {
        gameObject.SetActive(false);
        getBonus?.Invoke(BonusId);
    }

    public void GetAdsBonus()
    {
        gameObject.SetActive(false);
        getBonus?.Invoke(BonusId);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        closePanel?.Invoke();
    }
}
