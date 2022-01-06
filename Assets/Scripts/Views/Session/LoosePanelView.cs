using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoosePanelView : MonoBehaviour
{
    [SerializeField] private Image filledCircle;
    [SerializeField] private Text persent;


    public void InitView()
    {
        StartCoroutine(ChangePercent(0.75f));
    }


    void Update()
    {
        
    }

    private IEnumerator ChangePercent(float _current)
    {
        filledCircle.fillAmount = 0;
        while (filledCircle.fillAmount <= _current)
        {
            filledCircle.fillAmount += 0.0025f;
            persent.text = (filledCircle.fillAmount * 100).ToString("N0") + " %";
            yield return null;
        }
        
    }

}
