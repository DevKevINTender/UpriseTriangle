using Controlers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MainMenuCore;

namespace Views.MainMenu
{
    public class MMSegmentButtonView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text currentSegmentCountText;
        private buttonDelegate buttonPush;
    
        public void InitView( buttonDelegate buttonPush, int currenSegmentCount)
        {
            this.buttonPush = buttonPush;
            currentSegmentCountText.text = "x" + currenSegmentCount + "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            buttonPush?.Invoke();
        }
    }
}