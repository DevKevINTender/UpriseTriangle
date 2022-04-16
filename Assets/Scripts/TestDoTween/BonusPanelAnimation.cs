using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BonusPanelAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform freeBonus;
    [SerializeField] private Transform adsBonus;
    [SerializeField] private Transform backBtn;
    TweenCallback callback;
    Tween myTween;
    public void OpenPanelAnim()
    {
        TweenCallback openCallback = () => { myTween = adsBonus.DOScale(Vector3.one * 1.1f, 1f).SetLoops(-1, LoopType.Yoyo);  };
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence();    
        openPanel.Append( transform.DOLocalMove(Vector3.zero, 1));
        openPanel.AppendInterval(0.25f);
        openPanel.Append(freeBonus.DOScale(Vector3.one,0.05f));
        openPanel.Append(adsBonus.DOScale(Vector3.one, 0.05f));
        openPanel.Append( freeBonus.DOLocalMove(Vector3.zero, 0.5f));
        openPanel.Join( adsBonus.DOLocalMove(Vector3.zero, 0.5f));
        openPanel.AppendInterval(0.5f);
        openPanel.Append(backBtn.DOLocalMove(Vector3.zero, 0.5f)).OnComplete(openCallback);
    }

    public void ClosePanelAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        callback = () => { closeVoid?.Invoke(); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Join( backBtn.DOLocalMove(new Vector3(0,-1500), 0.5f));
        openPanel.Join( freeBonus.DOLocalMove(new Vector3(0,1500), 0.5f));
        openPanel.Join( adsBonus.DOLocalMove(new Vector3(0,1500), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOLocalMove(new Vector3(1500,0), 1).OnComplete(callback));
    }
    
    public void GetFreeBonusAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        callback = () => { closeVoid?.Invoke(); };
        TicketAnim(freeBonus, adsBonus);
    }
    
    public void GetAdsBonusAnim(MagnetBonusPanelView.ClosePanelDel closeVoid)
    {
        KillLoopTween(myTween);
        callback = () => { closeVoid?.Invoke(); };
        TicketAnim(adsBonus, freeBonus);
    }

    public void TicketAnim(Transform _choosenTicket, Transform _closedTicket)
    {
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence();
        openPanel.Append(backBtn.DOLocalMove(new Vector3(0, -1500), 0.5f));
        openPanel.Append(_choosenTicket.DOScale(new Vector3(0, 0, 0), 0.25f));
        openPanel.Append(_closedTicket.DOLocalMove(new Vector3(0, -1500), 0.5f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append(transform.DOLocalMove(new Vector3(1500, 0), 1).OnComplete(callback));
    }

    public void KillLoopTween(Tween _myTween)
    {
        _myTween.SetLoops(0);
        _myTween.Complete();
        _myTween.Kill();
    }

}
