using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMPersonButtonView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Text currentPersonIdText;
        private buttonDelegate buttonPush;
    
        public void InitView( buttonDelegate buttonPush, PersonScrObj currentPerson)
        {
            this.buttonPush = buttonPush;
            if (currentPerson.Id < 10)
            {
                currentPersonIdText.text = "0" + currentPerson.Id + "";
            }
            else
            {
                currentPersonIdText.text = currentPerson.Id + "";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}