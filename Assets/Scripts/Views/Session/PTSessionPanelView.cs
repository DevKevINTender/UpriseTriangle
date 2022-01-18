using Components.Session;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.Session
{
    public class PTSessionPanelView : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] private PTPersonComponent PersonObj;
        [SerializeField] private float sensitivity;
        [SerializeField] private Camera MainCamera;
        
        int touchCount;
        private Vector3 newPos;
        private Vector3 currentPos;

        private Vector3 touchPoint;
        //[SerializeField] private SessionCore SessionCore;
        public void InitView( )
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //if (SessionCore.isStart)
            {
                //SessionCore.StopPause();
            }
        }


        public void OnDrag(PointerEventData eventData)
        {
            touchPoint = eventData.pointerCurrentRaycast.screenPosition;
            float resolution = (MainCamera.pixelHeight / (2 * MainCamera.orthographicSize));
            //if (SessionCore.isStart)
            {
                if (touchCount != Input.touchCount)
                {
                    touchCount = Input.touchCount;
                    currentPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution, 0); 
                }
            
                if (Input.touchCount == 1) // комментится если необходимо тестировать игру в unity
                {
                    //newPos = new Vector3(touchPoint.x /192, touchPoint.y / 192 , 0);
                    newPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution  , 0); 
                    if (Vector3.Distance(currentPos, newPos) > 0.01f)
                    {
                        Vector3 distanceChange = (newPos - currentPos);
                        Vector3 personPos = PersonObj.transform.localPosition;
                        Vector3 checkFilterPos = personPos + distanceChange;
                        if (checkFilterPos.y < -4.0f) distanceChange = new Vector3(distanceChange.x, 0, 0);
                        if (checkFilterPos.y > 4.0f) distanceChange = new Vector3(distanceChange.x, 0, 0);
                        PersonObj.Move(distanceChange * sensitivity);
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
                    //SessionCore.StartPause();
                }
            }
            touchCount = 0;
        }
    }
}