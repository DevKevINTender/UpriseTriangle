using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCore : MonoBehaviour
{
    [SerializeField] private Text CoinsCountText;
    AsyncOperation async;
    void Start()
    {
        async = SceneManager.LoadSceneAsync(8);
        async.allowSceneActivation = false;
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
    }
    
    public void LoadScene(int id)
    {
        StartCoroutine(WaitUntil(1, id));
    }

    private IEnumerator WaitUntil(float time, int id)
    {
        yield return new WaitForSeconds(time);    
        //SceneManager.LoadScene(id);
        async.allowSceneActivation = true;
    }
}
