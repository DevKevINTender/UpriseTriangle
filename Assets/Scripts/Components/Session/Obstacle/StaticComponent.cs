using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StaticComponent : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Topper": { transform.localScale = new Vector3(0.1f, 0.1f); } break;
            case "Top": { transform.DOScale(Vector3.one, 0.2f); } break;
        }
    }
}
