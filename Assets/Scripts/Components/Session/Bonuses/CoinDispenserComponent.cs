using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinDispenserComponent : MonoBehaviour
{
    [SerializeField] private GameObject coinPb;
    private GameObject coinObj;

    private float y;
    private float x;
    public void SpawnCoin()
    {
        y = Random.Range(-2,2);
        coinObj = Instantiate(coinPb, transform);
        coinObj.transform.localPosition = Vector3.zero;
        coinObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localPosition.x * -1, y) * 20);
        coinObj.transform.localScale = Vector3.zero;
        coinObj.transform.DOScale(Vector3.one, 0.2f);
    }
}
