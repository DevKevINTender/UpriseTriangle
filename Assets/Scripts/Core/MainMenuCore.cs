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
        CoinsCountText.text = $"{CoinsControler.GetCoinsCount()}";
    }
    
    public void LoadScene(int id)
    {
        TransitionAnimation.CloseScene(0, id);

    }
}
