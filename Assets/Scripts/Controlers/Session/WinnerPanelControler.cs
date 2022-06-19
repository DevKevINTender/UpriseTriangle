using DOTweenAnimation.Global;
using ScriptableObjects;
using UnityEngine;

namespace Controlers.Session
{
    public class WinnerPanelControler : MonoBehaviour
    {
        [SerializeField] private int attempCount;
        [SerializeField] private int freeCoinCount;
        [SerializeField] private int adsCoinCount;
        [SerializeField] private int totalCoinCollect;
        [SerializeField] private WinnerPanelView winnerPanelView;
        [SerializeField] private CoinCollectorComponent coinCollectorComponent;
        [SerializeField] private TransitionAnimation TransitionPanelAnimation;
        [SerializeField] private int adsMultiple;
        private int currentId;
        private SkillScrObj skillInfo;
        public void InitControler(int currentId)
        {
            this.currentId = currentId;
            
            attempCount = LevelChooseControler.GetSessionAttempCount(currentId);
            freeCoinCount = LevelChooseControler.GetSessionWinReward(currentId);
            skillInfo = SkillStorageContoler.GetSkillById(SkillStorageContoler.GetCurrentSkill());
            adsCoinCount = freeCoinCount * adsMultiple;
            totalCoinCollect = coinCollectorComponent.GetSessionCoinTotalCollect();
            winnerPanelView.InitView(attempCount,freeCoinCount,adsCoinCount,totalCoinCollect,GetFreeBonus, GetAdBonus);
        }

        public void GetAdBonus()
        {
            // Проверка на наличие активного скила
            if (skillInfo.skillType == SkillScrObj.SkillType.LevelCompleteIncreaseCoin)
            {
                CoinsControler.UpcreaseCoins( (int)(adsCoinCount * skillInfo.skillValue));
            }
            else
            {    
                CoinsControler.UpcreaseCoins(adsCoinCount);
            }
        
            TransitionPanelAnimation.CloseSessionScene(0, "MainMenu");
        }

        public void GetFreeBonus()
        {
            if (skillInfo.skillType == SkillScrObj.SkillType.LevelCompleteIncreaseCoin)
            {
                CoinsControler.UpcreaseCoins( (int)(freeCoinCount * skillInfo.skillValue));
            }
            else
            {    
                CoinsControler.UpcreaseCoins(freeCoinCount);
            }
            TransitionPanelAnimation.CloseSessionScene(0, "MainMenu");
        }
    }
}