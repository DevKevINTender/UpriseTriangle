using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxStaticObstacleComponent : BoxObstacleComponent
{
    [SerializeField] private float reloadTime;
    internal override IEnumerator StartAction()
    {
        Coloring(alertColor, startTime);
        yield return new WaitForSeconds(alertTime);

        Coloring(endColor, changeColorTime);
        yield return new WaitForSeconds(changeColorTime);

        Coloring(boopColor, boopTime);
        Scalling(boopScale, boopTime);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(boopTime);

        Coloring(endColor, boopTime);
        Scalling(startScale, boopTime);
        yield return new WaitForSeconds(disappearTime);

        boxCollider.enabled = false;
        Scalling(Vector3.zero, boopTime);
        yield return new WaitForSeconds(boopTime);
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

    public override void SelfDestroy()
    {
        Debug.Log("Reload");
        StartCoroutine(Reload());
    }
}
