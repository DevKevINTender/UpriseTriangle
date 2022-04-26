using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BonusPanelAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform freeBonus;
    [SerializeField] private RectTransform adsBonus;
    [SerializeField] private RectTransform backBtn;
    public void OpenPanelAnim()
    {
        transform.gameObject.SetActive(true);
        mainPanel.anchoredPosition = new Vector2(Screen.currentResolution.width,0);
        freeBonus.anchoredPosition = new Vector2(0,Screen.currentResolution.height * 1.25f);
        adsBonus.anchoredPosition = new Vector2(0,Screen.currentResolution.height * 1.25f);
        backBtn.anchoredPosition = new Vector2(0,-Screen.currentResolution.height * 1.25f);
        
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
        
        TweenCallback callback = () =>
        {
            closeVoid?.Invoke();
            transform.gameObject.SetActive(false);
        };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Append( backBtn.DOLocalMove(new Vector3(0,-Screen.currentResolution.height * 1.25f), 0.5f));
        openPanel.Append( freeBonus.DOLocalMove(new Vector3(0,Screen.currentResolution.height * 1.25f), 0.5f));
        openPanel.Join( adsBonus.DOLocalMove(new Vector3(0,Screen.currentResolution.height * 1.25f), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOLocalMove(new Vector3(Screen.currentResolution.width,0), 1).OnComplete(callback));
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
