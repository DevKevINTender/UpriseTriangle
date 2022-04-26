using System.Collections;
using System.Collections.Generic;
using Controlers;
using DOTweenAnimation.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCore : MonoBehaviour
{
    [SerializeField] private Text CoinsCountText;
    [SerializeField] private TransitionAnimation TransitionAnimation;
    AsyncOperation async;
    void Start()
    {
        TransitionAnimation.OpenScene();
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
    }
    
    public void LoadScene(int id)
    {
        TransitionAnimation.CloseScene(0);
        StartCoroutine(WaitUntil(1.25f, id));
    }

    private IEnumerator WaitUntil(float time, int id)
    {
       
        yield return new WaitForSeconds(time); 
        
        //SceneManager.LoadScene(id);
        async.allowSceneActivation = true;
    }
}
