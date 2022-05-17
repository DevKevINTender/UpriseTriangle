using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxStaticObstacleComponent : BoxObstacleComponent
{
    [SerializeField] private float reloadTime;
    internal override IEnumerator StartAction()
    {
        StartCoroutine(Coloring(startColor, alertColor, startTime));
        yield return new WaitForSeconds(alertTime);

        StartCoroutine(Coloring(alertColor, endColor, changeColorTime));
        yield return new WaitForSeconds(changeColorTime);

        StartCoroutine(Coloring(endColor, boopColor, boopTime));
        StartCoroutine(Scaling(boopScale, boopTime));
        boxCollider.enabled = true;
        yield return new WaitForSeconds(boopTime);

        StartCoroutine(Coloring(boopColor, endColor, boopTime));
        StartCoroutine(Scaling(startScale, boopTime));
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }


    private IEnumerator Reload()
    {
        transform.DOScale(Vector3.zero, boopTime);
        boxCollider.enabled = false;
        yield return new WaitForSecondsRealtime(reloadTime);
        transform.DOScale(Vector3.one, boopTime);
        boxCollider.enabled = true;
    }

    internal override void SelfDestroy()
    {
        StartCoroutine(Reload());
    }
}
