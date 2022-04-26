using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MagnetBonusPanelAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform freeBonus;
    [SerializeField] private Transform adsBonus;
    [SerializeField] private Transform backBtn;
    public void OpenPanelAnim()
    {
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence();    
        openPanel.Append( transform.DOLocalMove(Vector3.zero, 1));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( freeBonus.DOLocalMove(Vector3.zero, 0.5f));
        openPanel.Append( adsBonus.DOLocalMove(Vector3.zero, 0.5f));
        openPanel.AppendInterval(0.5f);
        openPanel.Append( backBtn.DOLocalMove(Vector3.zero, 0.5f));
            
       
    }

    public void ClosePanelAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        TweenCallback callback = () => { closeVoid?.Invoke(); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Append( backBtn.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.Append( freeBonus.DOLocalMove(new Vector3(0,1500), 0.5f));
        openPanel.Join( adsBonus.DOLocalMove(new Vector3(0,1500), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOLocalMove(new Vector3(1500,0), 1).OnComplete(callback));
    }
    
    public void GetFreeBonusAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        TweenCallback callback = () => { closeVoid?.Invoke(); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Append( backBtn.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.Append( freeBonus.DOScale(new Vector3(0,0,0), 0.25f));
        openPanel.Append( adsBonus.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOLocalMove(new Vector3(1500,0), 1).OnComplete(callback));
    }
    
    public void GetAdsBonusAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        TweenCallback callback = () => { closeVoid?.Invoke(); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Append( backBtn.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.Append( adsBonus.DOScale(new Vector3(0,0,0), 0.25f));
        openPanel.Append( freeBonus.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append(transform.DOLocalMove(new Vector3(1500, 0), 1).OnComplete(callback));
    }

}
