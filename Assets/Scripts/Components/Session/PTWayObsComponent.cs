using System;
using System.Collections;
using UnityEngine;

public class PTWayObsComponent : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float Target = -7;
    private Vector3 targetPos;
            
    [SerializeField] internal float TimerStart;
    [SerializeField] internal float timeStartChangeColor;
    [SerializeField] internal float colorChangeTime;
    [SerializeField] private float DestroyTime = 0.5f;

    [SerializeField] private Color32 colorRed;
    [SerializeField] private SpriteRenderer waySprite;
    [SerializeField] private float obstacleSpeed = 10;
        
    public void OnEnable()
    {
        waySprite.color = new Color32(36,38,46,0);
    }
        
    public void ObstacleInit()
    {
        targetPos = new Vector3(0, Target, 0);
        StartCoroutine(TimeStartCor());
    }

    IEnumerator TimeStartCor()
    {
        float timer = TimerStart;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        waySprite.color = new Color32(36,38,46,255);
        yield return new WaitForSeconds(timeStartChangeColor);
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        Color startColor = waySprite.color;
        float ElapsedTime = 0.0f;
        while (ElapsedTime < colorChangeTime)
        {
            ElapsedTime += Time.deltaTime;
            waySprite.color = Color.Lerp(startColor, colorRed, (ElapsedTime / colorChangeTime));
            yield return null;
        }
        StartCoroutine(TimeMoveCor());
    }


    IEnumerator TimeMoveCor()
    {
        while (obstacle.transform.localPosition != targetPos)
        {
            obstacle.transform.localPosition = Vector3.MoveTowards(obstacle.transform.localPosition, targetPos,
                Time.deltaTime * obstacleSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }
}