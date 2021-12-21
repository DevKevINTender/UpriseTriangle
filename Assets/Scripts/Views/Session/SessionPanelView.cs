using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.Session
{
    public class SessionPanelView : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] private GameObject PersonObj;
        [SerializeField] private Vector3 newPos;
        [SerializeField] private Vector3 currentPos;

        [SerializeField]
        int touchCount;

        private int lastFingerIndex = 0;
        private int lastIndex = 0;

        [SerializeField]
        private SessionCore SessionCore;
        public void InitView( )
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (SessionCore.isStart)
            {
                transform.GetComponent<Image>().color = new Color32(26, 27, 33, 0);
                SessionCore.StopPause();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (SessionCore.isStart)
            {
                if (touchCount != Input.touchCount)
                {
                    touchCount = Input.touchCount;
                    currentPos = eventData.pointerCurrentRaycast.worldPosition;
                }
            
                if (Input.touchCount == 1)
                {
                    newPos = eventData.pointerCurrentRaycast.worldPosition;
                    if (Vector3.Distance(currentPos, newPos) > 0.01f)
                    {
                        PersonObj.transform.position += newPos - currentPos;
                        currentPos = newPos;
                    }
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (SessionCore.isStart)
            {
                if (Input.touchCount == 1)
                {
                    transform.GetComponent<Image>().color = new Color32(26, 27, 33, 200);
                    SessionCore.StartPause();
                }
               
            }
            touchCount = 0;
        }
    }
}