using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Session
{
    public class SessionPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject PersonObj;
        [SerializeField] private Vector3 newPos;
        [SerializeField] private bool isStart;
        [SerializeField] private Vector3 currentPos;

        private int tochesCount;
        public void FixedUpdate()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    tochesCount++;
                    newPos = Camera.main.ScreenToWorldPoint(touch.position);
                    currentPos = Camera.main.ScreenToWorldPoint(touch.position);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    newPos = Camera.main.ScreenToWorldPoint(touch.position);
                    if (Vector3.Distance(currentPos, newPos) > 0.01f)
                    {
                        if (newPos != new Vector3(0, 0, 0))
                        {
                            PersonObj.transform.position += new Vector3((newPos.x - currentPos.x),(newPos.y - currentPos.y),0);
                            currentPos = newPos;
                        }

                    }
                }
               
            }
        }

/*
        public void OnBeginDrag(PointerEventData eventData)
        {
            
            if (Input.touchCount == 1) 
            {
                currentPos = eventData.pointerCurrentRaycast.worldPosition;
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.touchCount == 1)
            {
                newPos = eventData.pointerCurrentRaycast.worldPosition;

                if (Vector3.Distance(currentPos, newPos) > 0.01f)
                {
                    if (newPos != new Vector3(0, 0, 0))
                    {
                        PersonObj.transform.position += newPos - currentPos;
                        currentPos = newPos;
                    }

                }
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {
           
        }
        */
    }
}