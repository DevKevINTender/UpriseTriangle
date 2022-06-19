using UnityEngine;
using static BonusPanelAnimation;
namespace Views.Session.Bonuses
{
    public class ShieldBonusPanelView : MonoBehaviour
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
}