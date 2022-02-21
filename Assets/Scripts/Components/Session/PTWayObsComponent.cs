using System;
using System.Collections;
using UnityEngine;

public class PTWayObsComponent : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private SpriteRenderer waySprite;
    [SerializeField] internal float Target = -7;
    private Vector3 targetPos;
    private Color startColor;
            
    [SerializeField] internal float appeartime;
    [SerializeField] internal float scaleSpeed;

    [SerializeField] private float DestroyTime = 0.5f;

    [SerializeField] internal float obstacleSpeed = 10;
        
    public void OnEnable()
    {
        startColor = waySprite.color;
        transform.localScale = new Vector3(1, 0);
    }
        
    public void ObstacleInit()
    {
        targetPos = new Vector3(0, Target, 0);
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        float timer = appeartime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Scalling());
    }

    IEnumerator Scalling()
    {
        while (transform.localScale.y < 1)
        {
            transform.localScale += new Vector3(0, Time.deltaTime * scaleSpeed, 0);
            yield return null;
        }
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        while (obstacle.transform.localPosition.y > targetPos.y + 1f)
        {
            obstacle.transform.localPosition = Vector3.Lerp(obstacle.transform.localPosition, targetPos,
                Time.deltaTime * obstacleSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(DestroyTime);
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        float t = 0;
        while (t < 1)
        { 
            waySprite.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), t);
            t += Time.deltaTime * 2;
            yield return null;
        }     
        Destroy(gameObject);
    }
}