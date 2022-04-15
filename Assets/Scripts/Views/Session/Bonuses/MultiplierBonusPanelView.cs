﻿using UnityEngine;

namespace Views.Session.Bonuses
{
    public class MultiplierBonusPanelView : MonoBehaviour
    {
        private int BonusId = 3;
        private int BonusCount = 1;
    
        public delegate void GetBonusDel(int id, int count);
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
            getBonus?.Invoke(BonusId, BonusCount);
        }

        public void GetAdsBonus()
        {
            gameObject.SetActive(false);
            getBonus?.Invoke(BonusId, BonusCount);
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
            closePanel?.Invoke();
        }
    }
}