using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareObstacleComponent : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("Timings")]
    [SerializeField] float startTime;
    [SerializeField] float changeColorTime;
    [SerializeField] float boopTime;
    [SerializeField] float boopScale;
    [SerializeField] float disappearTime;
    [Header("Colors")]
    [SerializeField] Color endColor;
    [SerializeField] Color boopColor;
    private Color startColor;

    void Start()
    {
        startColor = spriteRenderer.color;
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(Coloring(startColor, endColor, changeColorTime));
        yield return new WaitForSeconds(changeColorTime);
        StartCoroutine(Coloring(endColor, boopColor, boopTime));
        StartCoroutine(Scaling(boopScale, boopTime));
        yield return new WaitForSeconds(boopTime);
        StartCoroutine(Coloring(boopColor, startColor, disappearTime));
        StartCoroutine(Scaling(0, disappearTime));
        yield return new WaitForSeconds(disappearTime);
    }

    private IEnumerator Coloring(Color startColor, Color endColor, float time)
    {
        float step = 0;
        while (step < 1)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, step);
            step += Time.deltaTime / time;
            yield return null;
        }     
    }

    private IEnumerator Scaling(float boopScale, float time)
    {
        float stepTime = 0;
        float stepScale = Time.fixedDeltaTime / (time / (boopScale - transform.localScale.x));
        while (stepTime < 1)
        {
            transform.localScale += Vector3.one * stepScale;
            stepTime += Time.fixedDeltaTime / time;
            yield return null;
        }
    }
}
