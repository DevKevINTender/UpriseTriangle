using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeAnimation : MonoBehaviour
{
    public void DoShake()
    {
        transform.DOShakePosition(0.5f,0.3f,15,0,false,true);
    }
}
