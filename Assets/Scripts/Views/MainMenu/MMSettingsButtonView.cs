using UnityEngine;
using UnityEngine.EventSystems;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMSettingsButtonView : MonoBehaviour, IPointerDownHandler
    {
        private buttonDelegate buttonPush;
    
        public void InitView(buttonDelegate buttonPush)
        {
            this.buttonPush = buttonPush;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}