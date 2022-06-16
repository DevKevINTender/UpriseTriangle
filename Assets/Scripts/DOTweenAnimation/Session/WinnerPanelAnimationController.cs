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
    [SerializeField] private BeastFlashingAnimation beastFlashing;

    private Sequence winnerPanelAnim;
    private Sequence winnerPanelAnimSecond;

    public void Start()
    {
        Top.anchoredPosition = new Vector2(0, 750);
        Bottom.anchoredPosition = new Vector2(0, -750);
        ticket.localScale = Vector3.zero;
        ticketAD.localScale = Vector3.zero;
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
        winnerPanelAnimSecond.Join(Top.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack));
        winnerPanelAnimSecond.Join(Bottom.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack));
        winnerPanelAnimSecond.Join(ticket.DORotate(new Vector3(0, 0, 0), 1.3f));
        winnerPanelAnimSecond.Join(ticketAD.DORotate(new Vector3(0, 0, 0), 1.3f));
        winnerPanelAnimSecond.Join(ticket.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 1f));
        winnerPanelAnimSecond.Join(ticketAD.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 1.3f));                            
    }
}
