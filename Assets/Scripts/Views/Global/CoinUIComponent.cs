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
    private Coroutine Decreasecoroutine = null;
    private bool increaseDelayWorking;
    private bool decreaseDelayWorking;
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
        if (transform.gameObject.activeInHierarchy)
        {
            if (increaseDelayWorking)
            {
                StopCoroutine(Increasecoroutine);
            }
            Increasecoroutine = StartCoroutine(IncreaseDelay(coinsCount));
            transform.GetComponent<Text>().text = $"{CoinsControler.GetCoinsCount()}";
        }
    }

    public void CoinDecreaseView(int coinsCount)
    {
        if (transform.gameObject.activeInHierarchy)
        {
            if (decreaseDelayWorking)
            {
                StopCoroutine(Decreasecoroutine);
            }
            Decreasecoroutine = StartCoroutine(DecreaseDelay(coinsCount));
            transform.GetComponent<Text>().text = $"{CoinsControler.GetCoinsCount()}";
        }
    }

    private IEnumerator DecreaseDelay(int coinsCount)
    {
        decreaseDelayWorking = true;
        totalCoinsCount += coinsCount;
        yield return new WaitForSeconds(0.3f);
        coinsManipulateText = SpawnCoinsChangeView();
        coinsManipulateText.color = decreaseColor;
        coinsManipulateText.text = "-" + totalCoinsCount;
        decreaseDelayWorking = false;
        totalCoinsCount = 0;
    }

    private IEnumerator IncreaseDelay(int coinsCount)
    {
        increaseDelayWorking = true;
        totalCoinsCount += coinsCount;
        yield return new WaitForSeconds(0.5f);
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
