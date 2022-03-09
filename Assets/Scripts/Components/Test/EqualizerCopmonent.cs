using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualizerCopmonent : MonoBehaviour
{
    [SerializeField] private List<GameObject> leftList = new List<GameObject>();
    [SerializeField] private List<GameObject> rightList = new List<GameObject>();

    [SerializeField] private float TestTimer;
    public void Start()
    {
        for (int i = 0; i < leftList.Count; i++)
        {
            UseEqulizerElement("left", i, 5, 10f, 0, 10f);
        }
    }

    public void UseEqulizerElement(string side,int id, float apmlitude, float timeUp,float timHold, float timeDown)
    {
        List<GameObject> sideList = side == "left" ? leftList : rightList;
        StartCoroutine(upCur(timeUp, timHold, timeDown, apmlitude, sideList[id]));
    }

    private IEnumerator upCur(float timeUp,float timeHold, float timeDown, float amplitude, GameObject EqualizerElement)
    {
        float curTimer = 0;
        while (curTimer <= timeUp)
        {
            curTimer += 0.001f;
            double scale = Math.Round(( curTimer / timeUp), 2);
            Debug.Log(scale);
            EqualizerElement.transform.localScale = new Vector3(1,1,1) + new Vector3(((float)scale)*amplitude,0,0);
            yield return null;
        }
        StartCoroutine(holdCur(timeHold, timeDown, amplitude, EqualizerElement));
    }
    private IEnumerator holdCur(float timeHold, float timeDown, float amplitude, GameObject EqualizerElement)
    {
        float curTimer = timeHold;
        while (curTimer >= 0)
        {
            curTimer -= 0.001f;
            yield return null;
        }
        StartCoroutine(downCur(timeDown, amplitude, EqualizerElement));
    }
    
    private IEnumerator downCur(float timeDown,  float amplitude, GameObject EqualizerElement)
    {
        float curTimer = timeDown;
        while (curTimer >= 0)
        {
            curTimer -= 0.001f;
            double scale = Math.Round(( curTimer / timeDown), 2);
            EqualizerElement.transform.localScale = new Vector3(1,1,1) + new Vector3(((float)scale)*amplitude,0,0);
            yield return null;
        }
    }
}
