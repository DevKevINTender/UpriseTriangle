using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinBombComponent : MonoBehaviour
{
    [SerializeField] private GameObject coinPb;
    [SerializeField] private int coinsCount;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private GameObject coinObj;
    private float angle;
    private float x;
    private float y;

    private float speed;

    private void Start()
    {
        Invoke("Spawn", 2f);
    }

    public void Spawn()
    {
        angle = 360 / coinsCount;
        for (int i = 0; i < coinsCount; i++)
        {
            x = Mathf.Cos(angle * i * Mathf.PI / 180);
            y = Mathf.Sin(angle * i * Mathf.PI / 180);
            coinObj = Instantiate(coinPb, transform);
            coinObj.name = i.ToString();
            coinObj.transform.localPosition = new Vector3(x, y) * 0.1f;
            speed = Random.Range(minSpeed, maxSpeed);
            coinObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * speed);
            coinObj.transform.localScale = Vector3.zero;
            coinObj.transform.DOScale(Vector3.one, 0.2f);
        }
    }
}
