using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.Session
{
    public class SessionPanelView : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] private GameObject PersonObj;
        [SerializeField] private float sensitivity;
        
        int touchCount;
        private Vector3 newPos;
        private Vector3 currentPos;

        [SerializeField] private SessionCore SessionCore;
        public void InitView( )
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            //if (SessionCore.isStart)
            {
                SessionCore.StopPause();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            //if (SessionCore.isStart)
            {
                if (touchCount != Input.touchCount)
                {
                    touchCount = Input.touchCount;
                    currentPos = eventData.pointerCurrentRaycast.worldPosition;
                }
            
                if (Input.touchCount == 1) // комментится если необходимо тестировать игру в unity
                {
                    newPos = eventData.pointerCurrentRaycast.worldPosition;
                    if (Vector3.Distance(currentPos, newPos) > 0.01f)
                    {
                        Vector3 distanceChange = newPos - currentPos;
                        Vector3 personPos = PersonObj.transform.position;
                        Vector3 checkFilterPos = personPos + distanceChange;                       
                        if (checkFilterPos.x < -2.0f) distanceChange = new Vector3(0, distanceChange.y, 0);
                        if (checkFilterPos.x > 2.0f) distanceChange = new Vector3(0, distanceChange.y, 0);
                        PersonObj.transform.position += distanceChange * sensitivity;
                        PersonObj.transform.rotation = Quaternion.Euler(0, 0, PersonObj.transform.position.x * 3);
                        currentPos = newPos;         
                    }
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if (SessionCore.isStart)
            {
                if (Input.touchCount == 1)
                {
                    SessionCore.StartPause();
                }
            }
            touchCount = 0;
        }
    }
}