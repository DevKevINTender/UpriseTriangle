using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BonusPanelAnimation;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.UI;

public class MagnetBonusPanelView : MonoBehaviour, IRewardedVideoAdListener
{
    public BonusPanelAnimation bonusPanelAnimation;
    public Text info;
    public GetBonusDel getBonus;
    public ClosePanelDel closePanel;
    public Image AdsBonus;
    public bool isLookFull;
    public void InitView(GetBonusDel getBonus, ClosePanelDel closePanel)
    {
        this.getBonus = getBonus;
        this.closePanel = closePanel;
        Appodeal.setRewardedVideoCallbacks(this);
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            AdsBonus.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            AdsBonus.color = new Color32(255, 255, 255, 25);
        }
        bonusPanelAnimation.OpenPanelAnim();
    }

    public void GetFreeBonus()
    {
        bonusPanelAnimation.GetFreeBonusAnim(getBonus);
    }
    
    public void GetAdsBonus()
    {
        if(Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) {
            info.text = "isloaded";
            bool test = Appodeal.show(Appodeal.REWARDED_VIDEO);	
            info.text = "ishowed: " + test;
        }
        
    }
    
    #region Rewarded Video callback handlers

//Called when rewarded video was loaded (precache flag shows if the loaded ad is precache).
    public void onRewardedVideoLoaded(bool isPrecache) 
    { 
        
    } 

// Called when rewarded video failed to load
    public void onRewardedVideoFailedToLoad() 
    { 

    } 

// Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative)
    public void onRewardedVideoShowFailed() 
    { 
        
    } 

// Called when rewarded video is shown
    public void onRewardedVideoShown() 
    { 
        
    } 

// Called when reward video is clicked
    public void onRewardedVideoClicked()
    { 
        
    } 

// Called when rewarded video is closed
    public void onRewardedVideoClosed(bool finished) 
    {
        if (finished)
        {
            bonusPanelAnimation.GetAdsBonusAnim(getBonus);
            info.text = "isGetAdReward";
        }
        else
        {
            bonusPanelAnimation.GetFreeBonusAnim(getBonus);
            info.text = "isGetFreeReward";
        }
    }

// Called when rewarded video is viewed until the end
    public void onRewardedVideoFinished(double amount, string name)
    {
      
    }

//Called when rewarded video is expired and can not be shown
    public void onRewardedVideoExpired() 
    { 
       
    }
 
    #endregion
}
