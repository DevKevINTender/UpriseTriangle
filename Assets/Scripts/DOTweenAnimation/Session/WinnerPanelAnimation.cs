using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DOTweenAnimation.Global;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerPanelAnimation : MonoBehaviour
{
    public TransitionAnimation TransitionAnimation;
    [Header("Panels")]
    public RectTransform mainPanel;
    public RectTransform topPanel;
    public RectTransform midlePanel;
    public RectTransform bottomPanel;
    [Header("Info")]
    public RectTransform LevelItem;
    public RectTransform freeBonus;
    public RectTransform adsBonus;
    public Sequence winnerPanelAnim;
    [Header("Test")]
    // Start is called before the first frame update
    public Vector3 freeBonusStartPos;
    public Vector3 adsBonusStartPos;
    [Header("Lights")]
    public RectTransform lightsFirst;
    public RectTransform lightsSecond;

    void Start()
    {
        DOTween.defaultTimeScaleIndependent = true;
    }
    
    [ContextMenu("Open Menu")]
    public void OpenPanelAnim()
    {
        mainPanel.anchoredPosition = new Vector2(1500,0);
        topPanel.anchoredPosition = new Vector2(0,1500);
        bottomPanel.anchoredPosition = new Vector2(0,-1500);
        
        midlePanel.localScale = Vector3.zero;
        topPanel.localScale = Vector3.one;
        bottomPanel.localScale = Vector3.one;
        LevelItem.localScale = Vector3.one;

        freeBonus.localScale = Vector3.zero;
        adsBonus.localScale = Vector3.zero;
        
        lightsFirst.localScale = Vector3.zero;
        lightsSecond.localScale = Vector3.zero;
        
        winnerPanelAnim.Kill();
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.AppendInterval(1f);
        winnerPanelAnim.Append(mainPanel.DOAnchorPos(Vector2.zero, 0.5f));
        winnerPanelAnim.Append(topPanel.DOAnchorPos(Vector2.zero, 0.5f));
        winnerPanelAnim.Join(bottomPanel.DOAnchorPos(Vector2.zero, 0.5f));
        winnerPanelAnim.Join(midlePanel.DOScale(Vector2.one, 0.5f));
        winnerPanelAnim.AppendInterval(1f);
        winnerPanelAnim.Append(LevelItem.DOScale(Vector2.zero, 0.5f));
        winnerPanelAnim.AppendCallback(() =>
        {
            LevelItem.gameObject.SetActive(false);
            freeBonus.gameObject.SetActive(true);
            adsBonus.gameObject.SetActive(true);
        });
        winnerPanelAnim.Append(freeBonus.DOScale(Vector2.one, 0.5f));
        winnerPanelAnim.Join(adsBonus.DOScale(Vector2.one, 0.5f));
        
    }
    [ContextMenu("Close Menu")]
    public void ClosePanelAnim()
    {
        adsBonus.localScale = Vector3.zero;
        freeBonus.localScale = Vector3.zero;
        
        winnerPanelAnim.Kill();
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.AppendCallback(() =>
            {
                TransitionAnimation.CloseScene(0, 0);
            });
    }

    public void ChooseAdsBonus()
    {
        lightsFirst.localScale = Vector3.zero;
        lightsSecond.localScale = Vector3.zero;
        
        winnerPanelAnim.Kill();
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.Append(topPanel.DOAnchorPos(new Vector3(0, 1500), 1f));
        winnerPanelAnim.Join(bottomPanel.DOAnchorPos(new Vector3(0, -1500), 1f));
        winnerPanelAnim.AppendCallback(() =>
        {
            lightsFirst.gameObject.SetActive(true);
            lightsSecond.gameObject.SetActive(true);
        });
        
        winnerPanelAnim.Append(adsBonus.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1f));
        winnerPanelAnim.Join(freeBonus.DOAnchorPos(new Vector2(-Screen.currentResolution.width, 0), 1f));
        winnerPanelAnim.Join(adsBonus.DOAnchorPos(Vector2.zero, 0.25f ));
        
        winnerPanelAnim.Join(lightsFirst.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Join(lightsSecond.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Join(lightsFirst.DORotate(new Vector3(0,0,180), 1f));
        winnerPanelAnim.Join(lightsSecond.DORotate(new Vector3(0,0,180), 1f));
        
        winnerPanelAnim.AppendInterval(0.5f);
        winnerPanelAnim.Append(adsBonus.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.25f, 2));
        
        winnerPanelAnim.Append(lightsFirst.DOScale(Vector3.zero, 1f));
        winnerPanelAnim.Join(lightsSecond.DOScale(Vector3.zero, 1f));
        winnerPanelAnim.Join(lightsFirst.DORotate(new Vector3(0,0,0), 1f));
        winnerPanelAnim.Join(lightsSecond.DORotate(new Vector3(0,0,0), 1f));
        winnerPanelAnim.Join(adsBonus.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Append(adsBonus.DOScaleY(0, 0.25f));
        //.Join(adsBonus.DOScaleX(0, 0.5f));
        
        winnerPanelAnim.AppendInterval(0.25f).
            OnComplete(() => { ClosePanelAnim();});
    }

    public void ChooseFreeBonus()
    {
        lightsFirst.localScale = Vector3.zero;
        lightsSecond.localScale = Vector3.zero;
        
        winnerPanelAnim.Kill();
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.Append(topPanel.DOAnchorPos(new Vector3(0, 1500), 1f));
        winnerPanelAnim.Join(bottomPanel.DOAnchorPos(new Vector3(0, -1500), 1f));
        winnerPanelAnim.AppendCallback(() =>
        {
            lightsFirst.gameObject.SetActive(true);
            lightsSecond.gameObject.SetActive(true);
        });
        
        winnerPanelAnim.Append(freeBonus.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1f));
        winnerPanelAnim.Join(adsBonus.DOAnchorPos(new Vector2(Screen.currentResolution.width, 0), 1f));
        winnerPanelAnim.Join(freeBonus.DOAnchorPos(Vector2.zero, 0.25f ));
        
        winnerPanelAnim.Join(lightsFirst.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Join(lightsSecond.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Join(lightsFirst.DORotate(new Vector3(0,0,180), 1f));
        winnerPanelAnim.Join(lightsSecond.DORotate(new Vector3(0,0,180), 1f));
        
        winnerPanelAnim.AppendInterval(0.5f);
        winnerPanelAnim.Append(freeBonus.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.25f, 2));
        
        winnerPanelAnim.Append(lightsFirst.DOScale(Vector3.zero, 1f));
        winnerPanelAnim.Join(lightsSecond.DOScale(Vector3.zero, 1f));
        winnerPanelAnim.Join(lightsFirst.DORotate(new Vector3(0,0,360), 1f));
        winnerPanelAnim.Join(lightsSecond.DORotate(new Vector3(0,0,360), 1f));
        winnerPanelAnim.Join(freeBonus.DOScale(Vector3.one, 1f));
        winnerPanelAnim.Append(freeBonus.DOAnchorPos(new Vector2(0, 1500), 1f));
        
        winnerPanelAnim.AppendInterval(0.25f).
            OnComplete(() => { ClosePanelAnim();});
    }
    
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
