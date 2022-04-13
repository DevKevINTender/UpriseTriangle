using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCore : MonoBehaviour
{
    [SerializeField] private Text CoinsCountText;
    [SerializeField] private Animator TransitionAnimator;
    AsyncOperation async;
    void Start()
    {
        async = SceneManager.LoadSceneAsync(3);
        async.allowSceneActivation = false;
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
    }
    
    public void LoadScene(int id)
    {
        TransitionAnimator.SetTrigger("IsOpen");
       StartCoroutine(WaitUntil(1, id));
    }

    private IEnumerator WaitUntil(float time, int id)
    {
       
        yield return new WaitForSeconds(time); 
        
        //SceneManager.LoadScene(id);
        async.allowSceneActivation = true;
    }
}
