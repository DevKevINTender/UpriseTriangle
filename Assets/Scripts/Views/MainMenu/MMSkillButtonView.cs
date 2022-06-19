using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMSkillButtonView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text currentSkillIdText;
        [SerializeField] private Text skillInfo;
        [SerializeField] private Text skillValue;
        private buttonDelegate buttonPush;
    
        public void InitView( buttonDelegate buttonPush, SkillScrObj currentSkill)
        {
            this.buttonPush = buttonPush;
            skillInfo.text = currentSkill.skillDescription;
            skillValue.text = currentSkill.skillValue+"%";
            currentSkillIdText.text = (currentSkill.Id < 10 ? "0" + currentSkill.Id : currentSkill.Id + "");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}