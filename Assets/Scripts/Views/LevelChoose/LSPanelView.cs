using Controlers;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.ChooseLevel
{
    public class LSPanelView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ChooseLevelCore ChooseLevelCoreObj;
        
        public GameObject BackBtn;

        public Vector3 startDragVector;
        public Vector3 endDragVector;
        

        public void InitView(ChooseLevelCore ChooseLevelCoreObj)
        {
            this.ChooseLevelCoreObj = ChooseLevelCoreObj;
        }

        public void BackToMenu()
        {
            ChooseLevelCoreObj.BackToMenu();
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
                ChooseLevelCoreObj.ShowPreviousPage();
            }
            else
            {
                ChooseLevelCoreObj.ShowNextPage();
            }
        }
    }
}