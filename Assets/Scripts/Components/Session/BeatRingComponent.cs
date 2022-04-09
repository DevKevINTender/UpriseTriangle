using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRingComponent : MonoBehaviour
{
    [SerializeField] private List<TimeSegment> timeSegment = new List<TimeSegment>();

    [SerializeField] private int currentStep = 0;
    [SerializeField] private float currentTime;
    [SerializeField] private float currentTimeGoal;
    [SerializeField] private float BPM = 125;
    void Start()
    {
        currentTimeGoal = timeSegment[currentStep].time;
        StepByStep();
    }

    void StepByStep()
    {
        if (currentTime >= currentTimeGoal)
        {
            currentStep++;
            currentTimeGoal = timeSegment[currentStep].time;
        }

        float timeToIncrease = 60 / BPM * timeSegment[currentStep].beat/2;
        StartCoroutine(IncreaseRing(timeToIncrease));
    }

    IEnumerator IncreaseRing(float timeToIncrease)
    {
        float timer = timeToIncrease;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1.1f, 1.1f, 1.1f), Time.deltaTime / timeToIncrease);
            currentTime += Time.deltaTime;
            Debug.Log("Incr");
            yield return null;
            
        }
        Debug.Log(timeToIncrease);
        StartCoroutine(DecreaseRing(timeToIncrease));

    }
    IEnumerator DecreaseRing(float timeToDecrease)
    {
        float timer = timeToDecrease;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime / timeToDecrease);

            currentTime += Time.deltaTime;
            Debug.Log("Decr");
            yield return null;
        }

        StepByStep();
    }
}

[Serializable]
class TimeSegment
{
    [Header("From previous to this time:")]
    public float time;
    public float beat;
}
