using System;
using System.Collections;
using UnityEngine;

namespace Components.Session
{
    public class GateObstacleComponent : MonoBehaviour
    {
        [SerializeField] private GameObject leftGate;
        [SerializeField] private GameObject rightGate;
        
        [SerializeField] private float TimerStart;
        [SerializeField] private float TimeStartMove;
        [SerializeField] private float DestroyTime;

        public void Start()
        {
            StartCoroutine(TimeStartCor());
        }
        
        IEnumerator TimeStartCor()
        {
            float timer = TimerStart;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                leftGate.transform.Rotate(new Vector3(0, 0,  Time.deltaTime*25));
                rightGate.transform.Rotate(new Vector3(0, 0,  -Time.deltaTime*25));
                yield return null;
            }
           
        }
    }
}