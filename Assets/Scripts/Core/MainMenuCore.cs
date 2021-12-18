using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCore : MonoBehaviour
{
    [SerializeField] private Text CoinsCountText;
    void Start()
    {
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
    }
    
    void Update()
    {
        
    }

    public void LoadScene(int id)
    {
        StartCoroutine(WaitUntil(1.3f, id)); //Магическое число для ожидания конца анимации перехода
        //SceneManager.LoadScene(id);
    }

    public void AddCoins()
    {
        CoinsControler.UpcreaseCoins(150);
    }

    private IEnumerator WaitUntil(float time, int id)
    {
        yield return new WaitForSeconds(time);    
        SceneManager.LoadScene(id);
    }
}
