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
    [SerializeField] private BeastFlashing beastFlashing;

    private Sequence winnerPanelAnim;

    public void Start()
    {
        TweenCallback callback = () =>
        {
            beastFlashing.StartAction(this);
        };
        winnerPanelAnim = DOTween.Sequence();
        winnerPanelAnim.Append(panelImage.DOFade(255, 0.5f)).OnComplete(callback);
    }

    public void BeastCallback()
    {
        SecondPart();
    }

    public void SecondPart()
    {
        Top.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
        Bottom.DOAnchorPosY(0, 1f).SetEase(Ease.OutBack);
    }
}
