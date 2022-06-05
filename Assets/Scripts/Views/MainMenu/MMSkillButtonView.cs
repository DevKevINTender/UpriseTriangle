using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMSkillButtonView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Text currentSkillIdText;
        private buttonDelegate buttonPush;
    
        public void InitView( buttonDelegate buttonPush, SkillScrObj currentSkill)
        {
            this.buttonPush = buttonPush;
            if (currentSkill.Id < 10)
            {
                currentSkillIdText.text = "0" + currentSkill.Id + "";
            }
            else
            {
                currentSkillIdText.text = currentSkill.Id + "";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}