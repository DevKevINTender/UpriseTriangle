using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanelAnimation : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int adsCount;
    [SerializeField] private int freeCount;
    public delegate void GetBonusDel(int id, int count,int type);
    public delegate void ClosePanelDel();
    
    // Start is called before the first frame update
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform freeBonus;
    [SerializeField] private RectTransform adsBonus;
    [SerializeField] private RectTransform backBtn;
    public void OpenPanelAnim()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence();
        openPanel.Append( transform.DOScale(Vector3.one, 0.5F)).SetEase(Ease.InCubic);
    }

   
    
    public void GetFreeBonusAnim(GetBonusDel getBonus)
    {
        TweenCallback callback = () => { getBonus?.Invoke(id,freeCount, 0); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence();
        openPanel.Append( freeBonus.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.25f));
        openPanel.Join(adsBonus.GetComponent<Image>().DOColor(new Color32(46,255,193,50), 0.25f));
        openPanel.Join( adsBonus.DOScale(new Vector3(0.9f,0.9f,0.9f), 0.25f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOScale(new Vector3(0,0), 0.25f).OnComplete(callback));
    }
    
    public void GetAdsBonusAnim(GetBonusDel getBonus)
    {
        TweenCallback callback = () => { getBonus?.Invoke(id,adsCount, 1); };
        
        DOTween.defaultTimeScaleIndependent = true;
        Sequence openPanel = DOTween.Sequence(); 
        openPanel.Append( adsBonus.DOScale(new Vector3(1.1f,1.1f,1.1f), 0.25f));
        openPanel.Join(freeBonus.GetComponent<Image>().DOColor(new Color32(255,255,255,50), 0.25f));
        openPanel.Join( freeBonus.DOScale(new Vector3(0.9f,0.9f,0.9f), 0.25f));
        openPanel.AppendInterval(0.25f);
        openPanel.Append( transform.DOScale(new Vector3(0,0), 0.25f).OnComplete(callback));
    }
    
    public void ClosePanelAnim(ClosePanelDel closeVoid)
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

}
