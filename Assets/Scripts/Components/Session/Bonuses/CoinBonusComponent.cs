using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonusComponent : MonoBehaviour
{
    public CoinBonusPanelView coinBonusPanelView;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
