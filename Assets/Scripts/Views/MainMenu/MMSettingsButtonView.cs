using UnityEngine;
using UnityEngine.EventSystems;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMSettingsButtonView : MonoBehaviour, IPointerClickHandler
    {
        private buttonDelegate buttonPush;
    
        public void InitView(buttonDelegate buttonPush)
        {
            this.buttonPush = buttonPush;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}