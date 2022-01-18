using System;
using System.Collections;
using UnityEngine;

namespace Components.Session
{
    public class PTWayObsComponent : MonoBehaviour
    {
       [SerializeField] private GameObject obstacle;
       [SerializeField] private float Target;
        private Vector3 targetPos;
            
        [SerializeField] private float TimerStart;
        [SerializeField] private float TimeStartMove;
        [SerializeField] private float DestroyTime;
            
        private float Timer;
            
        [SerializeField] private SpriteRenderer waySprite;
        [SerializeField] private float obstacleSpeed;
        [SerializeField] private bool isStartObstacle;
        
        public void OnEnable()
        {
            waySprite.color = new Color32(36,38,46,0);
        }
        
        public void ObstacleInit()
        {
            targetPos = new Vector3(0, Target, 0);
            //waySprite.color = new Color32(36,38,46,0);
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
            StartCoroutine(TimeStartMoveCor());
        }
        IEnumerator TimeStartMoveCor()
        {
            yield return new WaitForSecondsRealtime(TimeStartMove);
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

            StartCoroutine(TimeToDestroy(DestroyTime));
        }

        IEnumerator TimeToDestroy(float _time)
        {
            float timer = _time;
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}