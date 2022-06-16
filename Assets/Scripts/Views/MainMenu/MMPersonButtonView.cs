using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMPersonButtonView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text currentPersonIdText;
        [SerializeField] private Image personSkin;
        [SerializeField] private ParticleSystem effectSprite;
        private buttonDelegate buttonPush;
    
        public void InitView( buttonDelegate buttonPush, PersonScrObj currentPerson)
        {
            this.buttonPush = buttonPush;
            
            personSkin.sprite = currentPerson.PersonSkin;
            personSkin.SetNativeSize();
            effectSprite.textureSheetAnimation.SetSprite(0,currentPerson.Effect);
            
            
            if (currentPerson.Id < 10)
            {
                currentPersonIdText.text = "0" + currentPerson.Id + "";
            }
            else
            {
                currentPersonIdText.text = currentPerson.Id + "";
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}