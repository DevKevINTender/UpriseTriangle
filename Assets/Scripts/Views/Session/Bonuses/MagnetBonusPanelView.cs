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

    public bool isLookFull;
    public void InitView(GetBonusDel getBonus, ClosePanelDel closePanel)
    {
        this.getBonus = getBonus;
        this.closePanel = closePanel;
        Appodeal.setRewardedVideoCallbacks(this);
        
        bonusPanelAnimation.OpenPanelAnim();
    }

    public void GetFreeBonus()
    {
        bonusPanelAnimation.GetFreeBonusAnim(getBonus);
    }
    
    public void GetAdsBonus()
    {
        info.text = "tap";
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
        info.text = "Video loaded"; 
    } 

// Called when rewarded video failed to load
    public void onRewardedVideoFailedToLoad() 
    { 
        info.text = "Video failed"; 
    } 

// Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative)
    public void onRewardedVideoShowFailed() 
    { 
        info.text = "Video show failed"; 
    } 

// Called when rewarded video is shown
    public void onRewardedVideoShown() 
    { 
        info.text = "Video shown"; 
    } 

// Called when reward video is clicked
    public void onRewardedVideoClicked()
    { 
        info.text = "Video clicked"; 
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
        info.text = "isGetReward";
    }

//Called when rewarded video is expired and can not be shown
    public void onRewardedVideoExpired() 
    { 
        info.text = "Video expired"; 
    }
 
    #endregion
}
