using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(gameObject);
    }

    internal IEnumerator Coloring(Color startColor, Color endColor, float time)
    {
        float step = 0;
        while (step < 1)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, step);
            step += Time.deltaTime / time;
            yield return null;
        }     
    }

    internal IEnumerator Scaling(Vector3 boopScale, float time)
    {
        float stepTime = 0;
        float stepScaleX = Time.deltaTime / (time / (boopScale.x - transform.localScale.x));
        float stepScaleY = Time.deltaTime / (time / (boopScale.y - transform.localScale.y));
        while (stepTime < 1)
        {
            if(Time.timeScale > 0)
            if (transform.localScale.x + stepScaleX >= 0)
            {
                transform.localScale += new Vector3(stepScaleX, stepScaleY);                
            }
            else
            {
                transform.localScale = Vector3.zero;
            }            
            stepTime += Time.deltaTime / time;
            yield return null;
        }
    }
}
