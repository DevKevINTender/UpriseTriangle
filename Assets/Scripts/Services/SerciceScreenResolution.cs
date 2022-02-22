using System;
using Services;
using UnityEngine;

public class SerciceScreenResolution : MonoBehaviour
{
    private void Awake()
    {
        float width = ScreenSize.GetScreenToWorldWidth;
        transform.localScale =  Vector3.one * width / 5.625f;
        transform.localScale = new Vector3((float)Math.Round(transform.localScale.x, 2), (float)Math.Round(transform.localScale.y, 2)
            , (float)Math.Round(transform.localScale.z, 2));        
    }

    public float GetScaledGameSpeed()
    {
        return 0 * transform.localScale.x;
    }
}
