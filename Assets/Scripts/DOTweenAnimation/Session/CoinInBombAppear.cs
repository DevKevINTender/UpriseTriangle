using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinInBombAppear : MonoBehaviour
{
    Tween tween;
    public void Start()
    {
        tween = transform.DOScale(Vector3.one, 0.2f);
        tween.Play();
    }

    public void OnDestroy()
    {
        transform.DOKill();
    }

}
