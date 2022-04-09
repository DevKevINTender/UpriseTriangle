using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
{
    public BonusCollectorComponent BonusCollectorComponent;

    public int coinWithOutMultiplier;
    public int coinWithMultiplier;
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
        if (other.GetComponent<CoinComponent>())
        {
            if (BonusCollectorComponent.GetMultiplierBonusCount() > 0)
            {
                CoinsControler.UpcreaseCoins(coinWithOutMultiplier);
            }
            else
            {
                CoinsControler.UpcreaseCoins(coinWithMultiplier);
            }
            Destroy(other.gameObject);
        }
    }
}
