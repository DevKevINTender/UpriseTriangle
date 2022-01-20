using Components.Session;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersonMoveController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private PTPersonComponent PersonObj;
    [SerializeField] private float sensitivity;
    [SerializeField] private Camera MainCamera;

    int touchCount;
    float resolution;
    private Vector3 newPos;
    private Vector3 currentPos;

    private Vector3 touchPoint;
    Vector3 distanceChange;
    Vector3 personPos;
    Vector3 checkFilterPos;

    private bool crackLeft;
    private bool crackRight;

    public delegate void PauseDelegate();
    private PauseDelegate startPause;
    private PauseDelegate endPause;

    public void Start()
    {
        resolution = (MainCamera.pixelHeight / (2 * MainCamera.orthographicSize));
        Debug.Log(resolution);
    }

    public void InitComponent(PauseDelegate startpause, PauseDelegate endPause)
    {
        this.startPause = startpause;
        this.endPause = endPause;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        endPause?.Invoke();
    }


    public void OnDrag(PointerEventData eventData)
    {
        touchPoint = eventData.pointerCurrentRaycast.screenPosition;
        if (touchCount != Input.touchCount)
        {
            touchCount = Input.touchCount;
            currentPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution, 0);
        }
        if (Input.touchCount == 1)
        {
            newPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution, 0);
            if (Vector3.Distance(currentPos, newPos) > 0.01f)
            {
                distanceChange = (newPos - currentPos);
                personPos = PersonObj.transform.localPosition;
                checkFilterPos = personPos + distanceChange * sensitivity;
                distanceChange = CheckBorders(ref checkFilterPos, ref distanceChange);
                PersonObj.Move(distanceChange * sensitivity);
                currentPos = newPos;
            }
        }
    }

    public Vector3 CheckBorders(ref Vector3 _checkFilterPos, ref Vector3 _distanceChange)
    {
        if (_checkFilterPos.y < -4.0f || _checkFilterPos.y > 4.0f) return new Vector3(_distanceChange.x, 0, 0);
        if (_checkFilterPos.x > 1.8f)
        {
            crackRight = true;
            PersonObj.SetCrackRightActive(true);
            return new Vector3(0, _distanceChange.y, 0);
        }
        if (_checkFilterPos.x < -1.8f)
        {
            crackLeft = true;
            PersonObj.SetCrackLeftActive(true);
            return new Vector3(0, _distanceChange.y, 0);
        }
        if(crackLeft) PersonObj.SetCrackLeftActive(false);
        if (crackRight) PersonObj.SetCrackRightActive(false);
        return _distanceChange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            startPause?.Invoke();
        }
        touchCount = 0;
    }
}