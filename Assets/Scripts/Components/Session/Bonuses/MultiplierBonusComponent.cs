using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BonusPanelAnimation;

public class MultiplierBonusComponent : MonoBehaviour
{
    public BonusPanelAnimation bonusPanelAnimation;
    
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
        bonusPanelAnimation.GetFreeBonusAnim(getBonus);
    }
    
    public void GetAdsBonus()
    {
        bonusPanelAnimation.GetAdsBonusAnim(getBonus);
    }
}
