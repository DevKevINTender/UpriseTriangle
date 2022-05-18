using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxObstacleComponent : ObstacleComponent
{
    [SerializeField] internal SpriteRenderer spriteRenderer;
    [SerializeField] internal BoxCollider2D boxCollider;
    [Header("Timings")]
    [SerializeField] internal float startTemp;
    [SerializeField] internal float alertTemp;
    [SerializeField] internal float changeColorTemp;
    [SerializeField] internal float boopTemp;
    [SerializeField] internal float disappearTemp;
    [Header("Colors")]
    [SerializeField] internal Color endColor;
    [SerializeField] internal Color boopColor;
    [SerializeField] internal Color alertColor;

    [SerializeField] internal Vector3 boopScale;
    internal Color startColor;
    internal Vector3 startScale;


    internal float startTime;
    internal float alertTime;
    internal float changeColorTime;
    internal float boopTime;
    internal float disappearTime;

    public float TempToTiming(float _value)
    {
        return _value * (60f / 125f);
    }

    public float GetBoxTime()
    {
        return startTime + alertTime + changeColorTime; 
    }

    public void Start()
    {
        startTime = TempToTiming(startTemp);
        alertTime = TempToTiming(alertTemp);
        changeColorTime = TempToTiming(changeColorTemp);
        boopTime = TempToTiming(boopTemp);
        disappearTime = TempToTiming(disappearTemp);

        startColor = spriteRenderer.color;
        startScale = transform.localScale;
        boopScale = new Vector3(startScale.x * boopScale.x, startScale.y * boopScale.y);
        Active();
    }

    public void Active()
    {       
        StartCoroutine(StartAction());
    }

    internal virtual IEnumerator StartAction()
    {
        Coloring(alertColor, startTime);
        yield return new WaitForSeconds(alertTime);

        Coloring(endColor, changeColorTime);
        yield return new WaitForSeconds(changeColorTime);

        Coloring(boopColor, boopTime);
        Scalling(boopScale, boopTime);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(boopTime);

        Coloring(startColor, disappearTime);
        Scalling(Vector3.zero, disappearTime);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }


    internal void Scalling(Vector3 changeScale, float time)
    {
        transform.DOScale(changeScale, time);
    }

    internal void Coloring(Color endColor, float time)
    {
        spriteRenderer.DOColor(endColor, time);
    }
}
