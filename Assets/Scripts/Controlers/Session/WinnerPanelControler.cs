using DOTweenAnimation.Global;
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
        
        public void InitControler(int currentId)
        {
            this.currentId = currentId;
            
            attempCount = LevelChooseControler.GetSessionAttempCount(currentId);
            freeCoinCount = LevelChooseControler.GetSessionWinReward(currentId);
            adsCoinCount = freeCoinCount * adsMultiple;
            totalCoinCollect = coinCollectorComponent.GetSessionCoinTotalCollect();
            winnerPanelView.InitView(attempCount,freeCoinCount,adsCoinCount,totalCoinCollect,GetFreeBonus, GetAdBonus);
        }

        public void GetAdBonus()
        {
            CoinsControler.UpcreaseCoins(adsCoinCount);
            TransitionPanelAnimation.CloseSessionScene(0, "MainMenu");
        }

        public void GetFreeBonus()
        {
            CoinsControler.UpcreaseCoins(freeCoinCount);
            TransitionPanelAnimation.CloseSessionScene(0, "MainMenu");
        }
    }
}