using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBombComponent : MonoBehaviour
{
    [SerializeField] private GameObject coinPb;
    [SerializeField] private int coinsCount;
    private GameObject coinObj;
    private float angle;
    private float x;
    private float y;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        angle = 360 / coinsCount;
        for (int i = 0; i < coinsCount; i++)
        {
            x = 1f * Mathf.Cos(angle * i * Mathf.PI / 180);
            y = 1f * Mathf.Sin(angle * i * Mathf.PI / 180);
            Debug.Log("x: "+ x);
            Debug.Log("y: " + y);
            coinObj = Instantiate(coinPb, transform);
            coinObj.name = i.ToString();
            coinObj.transform.localPosition = new Vector3(x, y);
        }
    }
}
