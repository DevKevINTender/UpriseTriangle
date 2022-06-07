using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBonusPanelView : MonoBehaviour
{
    public BonusPanelAnimation bonusPanelAnimation;
    
    public delegate void GetBonusDel(int id, int count);
    public delegate void ClosePanelDel();
    
    public GetBonusDel getBonus;
    public ClosePanelDel closePanel;
    public void InitView(GetBonusDel getBonus, ClosePanelDel closePanel)
    {
        this.getBonus = getBonus;
        this.closePanel = closePanel;
        bonusPanelAnimation.OpenPanelAnim();
    }

    public void GetFreeBonus()
    {
        bonusPanelAnimation.GetFreeBonusAnim(BonusGeted);
    }
    
    public void GetAdsBonus()
    {
        bonusPanelAnimation.GetAdsBonusAnim(BonusGeted);
    }
    
    public void BonusGeted()
    {
        closePanel?.Invoke();
    }
 

    public void ClosePanel()
    {
        bonusPanelAnimation.ClosePanelAnim(closePanel);
    }
}
