using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayObstacleComponent : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Vector3 Target;
    
    [SerializeField] private float TimerStart;
    [SerializeField] private float TimeStartMove;
    //[SerializeField] private float DestroyTimer;
    
    private float Timer;
    
    [SerializeField] private SpriteRenderer waySprite;
    [SerializeField] private float obstacleSpeed;
    [SerializeField] private bool isStartObstacle;
    
    void Start()
    {
        waySprite.color = new Color32(36,38,46,0);
        StartCoroutine(TimeStartCor());
        StartCoroutine(TimeStartMoveCor());
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
        
    }
    IEnumerator TimeStartMoveCor()
    {
        float timer = TimeStartMove;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        StartCoroutine(TimeMoveCor());
    }
    IEnumerator TimeMoveCor()
    {
        while (obstacle.transform.localPosition != Target)
        {
            obstacle.transform.localPosition = Vector3.MoveTowards(obstacle.transform.localPosition, Target, Time.deltaTime * obstacleSpeed);

            yield return null;
        }
        Destroy(gameObject);
    }
}
