using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class CoinCollectorComponent : MonoBehaviour
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
        if (other.GetComponent<CoinComponent>())
        {
            CoinsControler.UpcreaseCoins(1);
            Destroy(other.gameObject);
        }
    }
}
