using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonusComponent : MonoBehaviour
{
    public CoinBonusPanelView coinBonusPanelView;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PersonComponent>())
        {
            //coinBonusPanelView.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
