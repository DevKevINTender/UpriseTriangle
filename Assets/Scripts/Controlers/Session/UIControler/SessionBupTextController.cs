using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionBupTextController : MonoBehaviour
{
    public float timeToAppear;
    public float timeTodisappear;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeToappear(timeToAppear));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeToappear(float _time)
    {
        yield return new WaitForSeconds(_time);
        obj.SetActive(true);
        StartCoroutine(TimeTodisappear(timeTodisappear));
    }

    IEnumerator TimeTodisappear(float _time)
    {
        yield return new WaitForSeconds(_time);
        obj.SetActive(false);
    }
}
