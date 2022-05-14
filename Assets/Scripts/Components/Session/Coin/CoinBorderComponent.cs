using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBorderComponent : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<CoinComponent>())
            other.transform.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
    }
}
