using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPanelView : MonoBehaviour
{
   public delegate void GetWinerBonus();
    
   public GetWinerBonus getFreeBonus;
   public GetWinerBonus getAdsBonus;
   
   [SerializeField] private Text attempText;
   [SerializeField] private Text freeCoinCountText;
   [SerializeField] private Text adsCoinCountText;
   [SerializeField] private Text totalCoinCollectText;
   public void InitView(int attempCount,int freeCoinCount,int adsCoinCount, int totalCoinCollect, GetWinerBonus getFreeBonus, GetWinerBonus getAdsBonus)
   {
      this.attempText.text = "" + attempCount;
      this.freeCoinCountText.text = "" + freeCoinCount;
      this.adsCoinCountText.text = "" + adsCoinCount;
      this.totalCoinCollectText.text = "ВСего собрано: " + totalCoinCollect;

      this.getFreeBonus = getFreeBonus;
      this.getAdsBonus = getAdsBonus;
   }
   
   public void GetAdBonus()
   {
      getAdsBonus?.Invoke();      
   }

   public void GetFreeBonus()
   {
      getFreeBonus?.Invoke();
   }
}
