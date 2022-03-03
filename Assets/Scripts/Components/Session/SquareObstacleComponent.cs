using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareObstacleComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    private SquareTimeService squareTimeService;
    [Header("Timings")]
    [SerializeField] private float startTime;
    [SerializeField] private float alertTime;
    [SerializeField] private float changeColorTime;
    [SerializeField] private float boopTime;
    [SerializeField] private float disappearTime;
    [Header("Colors")]
    [SerializeField] private Color endColor;
    [SerializeField] private Color boopColor;
    [SerializeField] private Color alertColor;

    [SerializeField] Vector3 boopScale;
    private Color startColor;
    private Vector3 startScale;

    public void Start()
    {
        squareTimeService = transform.parent.GetComponent<SquareTimeService>();
        startColor = spriteRenderer.color;
        startScale = transform.localScale;
        boopScale = new Vector3(startScale.x * boopScale.x, startScale.y * boopScale.y);
    }

    public void Active()
    {       
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        StartCoroutine(Coloring(startColor, alertColor, startTime));
        yield return new WaitForSeconds(alertTime);

        StartCoroutine(Coloring(alertColor, endColor, changeColorTime));
        yield return new WaitForSeconds(changeColorTime);

        StartCoroutine(Coloring(endColor, boopColor, boopTime));
        StartCoroutine(Scaling(boopScale, boopTime));
        boxCollider.enabled = true;
        yield return new WaitForSeconds(boopTime);

        StartCoroutine(Coloring(boopColor, startColor, disappearTime));
        StartCoroutine(Scaling(Vector3.zero, disappearTime));
        boxCollider.enabled = false;
        yield return new WaitForSeconds(disappearTime);

        spriteRenderer.color = startColor;
        transform.localScale = startScale;
        squareTimeService.AddToList(transform.GetComponent<SquareObstacleComponent>());
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

    private IEnumerator Scaling(Vector3 boopScale, float time)
    {
        float stepTime = 0;
        float stepScaleX = Time.deltaTime / (time / (boopScale.x - transform.localScale.x));
        float stepScaleY = Time.deltaTime / (time / (boopScale.y - transform.localScale.y));
        while (stepTime < 1)
        {
            transform.localScale += new Vector3(stepScaleX, stepScaleY);
            stepTime += Time.deltaTime / time;
            yield return null;
        }
    }
}
