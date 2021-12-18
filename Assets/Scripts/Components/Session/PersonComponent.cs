using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    [SerializeField] private SessionCore SessionCore;
    public void InitComponent(SessionCore SessionCore)
    {
        this.SessionCore = SessionCore;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            SessionCore.LoseSession();
        }
    }
}
