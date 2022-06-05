using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.MainMenu
{
    public class MenuPanelView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private MainMenuCore mainMenuCore;

        public Vector3 startDragVector;
        public Vector3 endDragVector;
        
        public void InitView(MainMenuCore mainMenuCore)
        {
            this.mainMenuCore = mainMenuCore;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            endDragVector = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startDragVector = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (endDragVector.x > startDragVector.x)
            {
                mainMenuCore.ShowPreviousPage();
            }
            else
            {
                mainMenuCore.ShowNextPage();
            }
        }
    }
}