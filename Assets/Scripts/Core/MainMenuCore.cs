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
        SceneManager.LoadScene(id);
    }
}
