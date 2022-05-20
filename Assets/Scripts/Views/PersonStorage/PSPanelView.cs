using System;
using Controlers;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.PersonStorage
{
    public class PSPanelView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
       private PersonStorageCore PersonStorageCoreObj;

       public GameObject BuyBtn;
       public GameObject PlayBtn;
       public GameObject BackBtn;

        public Vector3 startDragVector;
        public Vector3 endDragVector;
        

        public void InitView(PersonStorageCore PersonStorageCoreObj)
        {
            this.PersonStorageCoreObj = PersonStorageCoreObj;
        }
        
        public void OpenSegment()
        {
            PersonStorageCoreObj.OpenSegment();
        }
        
        public void BackToMenu()
        {
            PersonStorageCoreObj.BackToMenu();
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
                PersonStorageCoreObj.ShowPreviousPage();
            }
            else
            {
                PersonStorageCoreObj.ShowNextPage();
            }
        }
    }
}