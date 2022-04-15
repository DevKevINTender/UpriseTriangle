using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBonusPanelView : MonoBehaviour
{
    public MagnetBonusPanelAnimation MagnetBonusPanelAnimation;
    private int BonusId = 2;
    private int BonusCount = 1;
    
    public delegate void GetBonusDel(int id, int count);
    public delegate void ClosePanelDel();
    
    public GetBonusDel getBonus;
    public ClosePanelDel closePanel;
    public void InitView(GetBonusDel getBonus, ClosePanelDel closePanel)
    {
        this.getBonus = getBonus;
        this.closePanel = closePanel;
        MagnetBonusPanelAnimation.OpenPanelAnim();
    }

    public void GetFreeBonus()
    {
        MagnetBonusPanelAnimation.GetFreeBonusAnim(BonusGeted);
    }
    
    public void GetAdsBonus()
    {
        MagnetBonusPanelAnimation.GetAdsBonusAnim(BonusGeted);
    }
    
    public void BonusGeted()
    {
        closePanel?.Invoke();
    }
 

    public void ClosePanel()
    {
        MagnetBonusPanelAnimation.ClosePanelAnim(closePanel);
    }
}
