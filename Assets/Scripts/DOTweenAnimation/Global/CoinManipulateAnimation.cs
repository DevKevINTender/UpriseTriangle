using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinManipulateAnimation : MonoBehaviour
{
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        Sequence coinsLife = DOTween.Sequence();
        coinsLife.Append(transform.GetComponent<Text>().DOFade(1, 1f));
        coinsLife.Join(rectTransform.DOAnchorPosY(-75, 1f));
        coinsLife.Append(transform.GetComponent<Text>().DOFade(0, 1f));
        coinsLife.Join(rectTransform.DOAnchorPosY(-150, 1f));
    }

}
