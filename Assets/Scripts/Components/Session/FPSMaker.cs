using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSMaker : MonoBehaviour
{
    public Text text;
    float fps;
    float upd;
    private void Start()
    {
        upd = 0.2f;
    }
    void Update()
    {
        if (upd <= 0)
        {
            fps = 1.0f / Time.deltaTime;
            text.text = fps.ToString("F2");
            upd = 0.2f;
        }
        upd -= Time.deltaTime;
    }
}
