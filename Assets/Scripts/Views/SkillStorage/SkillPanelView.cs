using System;
using Controlers;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.PersonStorage
{
    public class SkillPanelView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
       private SkillStorageCore SkillStorageCoreObj;

       public GameObject BuyBtn;
       public GameObject PlayBtn;
       public GameObject BackBtn;

        public Vector3 startDragVector;
        public Vector3 endDragVector;
        

        public void InitView(SkillStorageCore SkillStorageCoreObj)
        {
            this.SkillStorageCoreObj = SkillStorageCoreObj;
        }
        
        public void OpenSegment()
        {
            SkillStorageCoreObj.OpenSegment();
        }
        
        public void BackToMenu()
        {
            SkillStorageCoreObj.BackToMenu();
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
                SkillStorageCoreObj.ShowPreviousPage();
            }
            else
            {
                SkillStorageCoreObj.ShowNextPage();
            }
        }
    }
}