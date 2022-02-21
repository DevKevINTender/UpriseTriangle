using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelyColliderController : MonoBehaviour
{
    public PolygonCollider2D playerCollider;
    public void SwichCollider()
    {
        if (playerCollider.enabled)
        { 
            playerCollider.enabled = false;
            return;
        }
        if (!playerCollider.enabled)
        {
            playerCollider.enabled = true;
            return;
        }
    }
}
