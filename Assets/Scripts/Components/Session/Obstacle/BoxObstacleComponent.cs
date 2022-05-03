using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObstacleComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Timings")]
    [SerializeField] private float startTemp;
    [SerializeField] private float alertTemp;
    [SerializeField] private float changeColorTemp;
    [SerializeField] private float boopTemp;
    [SerializeField] private float disappearTemp;
    [Header("Colors")]
    [SerializeField] private Color endColor;
    [SerializeField] private Color boopColor;
    [SerializeField] private Color alertColor;

    [SerializeField] Vector3 boopScale;
    private Color startColor;
    private Vector3 startScale;


    private float startTime;
    private float alertTime;
    private float changeColorTime;
    private float boopTime;
    private float disappearTime;

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
        Destroy(gameObject);
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
