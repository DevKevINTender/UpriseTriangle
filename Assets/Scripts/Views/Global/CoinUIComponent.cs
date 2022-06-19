using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controlers;


public class CoinUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject coinsManipulatePB;
    private GameObject coinsManipulateObj;
    private Text coinsManipulateText;
    [Header("Colors")]
    [SerializeField] private Color32 increaseColor;
    [SerializeField] private Color32 decreaseColor;

    private Coroutine Increasecoroutine = null;
    private bool increaseDelayWorking;
    private int totalCoinsCount;

    void Start()
    {
        increaseDelayWorking = false;
        transform.GetComponent<Text>().text = $"{CoinsControler.GetCoinsCount()}";
        CoinsControler.DecreaseCoinsEvent += CoinDecreaseView;
        CoinsControler.IncreaseCoinsEvent += CoinIncreaseView;
    }

    private void OnDestroy()
    {
        CoinsControler.DecreaseCoinsEvent -= CoinDecreaseView;
        CoinsControler.IncreaseCoinsEvent -= CoinIncreaseView;
    }

    public void CoinIncreaseView(int coinsCount)
    {
        if (increaseDelayWorking )
        {
           StopCoroutine(Increasecoroutine);
        }
        Increasecoroutine = StartCoroutine(IncreaseDelay(coinsCount));
    }

    public void CoinDecreaseView(int coinsCount)
    {
        coinsManipulateText = SpawnCoinsChangeView();
        coinsManipulateText.color = decreaseColor;
        coinsManipulateText.text = "-" + coinsCount;
    }

    private IEnumerator IncreaseDelay(int coinsCount)
    {
        increaseDelayWorking = true;
        totalCoinsCount += coinsCount;
        yield return new WaitForSeconds(0.3f);
        coinsManipulateText = SpawnCoinsChangeView();
        coinsManipulateText.color = increaseColor;
        coinsManipulateText.text = "+" + totalCoinsCount;
        increaseDelayWorking = false;
        totalCoinsCount = 0;
    }

    public Text SpawnCoinsChangeView()
    {
        coinsManipulateObj = Instantiate(coinsManipulatePB, transform);
        coinsManipulateObj.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        return coinsManipulateObj.GetComponent<Text>();
    }

}
