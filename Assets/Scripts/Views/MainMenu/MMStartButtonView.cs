using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

public class MMStartButtonView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Text currentLevelText;
    private buttonDelegate buttonPush;
    
    public void InitView( buttonDelegate buttonPush, int CurrentSessionLevelId)
    {
        this.buttonPush = buttonPush;
        if (CurrentSessionLevelId < 10)
        {
            currentLevelText.text = "0" + CurrentSessionLevelId + "";
        }
        else
        {
            currentLevelText.text = CurrentSessionLevelId + "";
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPush?.Invoke();
    }
}
