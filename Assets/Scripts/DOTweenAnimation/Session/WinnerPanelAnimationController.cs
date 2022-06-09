using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DOTweenAnimation.Global;
using UnityEngine;
using UnityEngine.UI;

public class WinnerPanelAnimationController : MonoBehaviour
{
    [SerializeField] private Image panelImage;
    [SerializeField] private RectTransform Top;
    [SerializeField] private RectTransform Bottom;
    [SerializeField] private RectTransform ticket;
    [SerializeField] private RectTransform ticketAD;
    [SerializeField] private BeastFlashing beastFlashing;

    private Sequence winnerPanelAnim;
    private Sequence winnerPanelAnimSecond;

    public void Start()
    {
        Invoke("PersonWin", 3f);
    }

    public void PersonWin()
    {
        TweenCallback callback = () =>
        {
            beastFlashing.StartAction(this);
        };
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.Append(panelImage.DOColor(new Color32(26, 27, 33, 255), 2f)).OnComplete(callback);
    }

    public void BeastCallback()
    {
        SecondPart();
    }

    public void SecondPart()
    {
        winnerPanelAnimSecond = DOTween.Sequence();
        Top.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
        Bottom.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
        ticket.DORotate(new Vector3(0, 0, 0), 1.3f);
        ticketAD.DORotate(new Vector3(0, 0, 0), 1.3f);
        ticket.DOScale(Vector3.one, 1.3f);
        winnerPanelAnimSecond.Append(ticketAD.DOScale(Vector3.one, 1f));                            
    }
}
