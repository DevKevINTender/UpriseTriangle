using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<CoinComponent>())
        {
            other.transform.GetComponent<CoinComponent>().pointToMove = transform;
        }
    }
}
