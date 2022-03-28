using System;
using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    public Transform pointToMove;
    void Start()
    {
        pointToMove = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointToMove.position, Time.deltaTime*15);
    }
}
