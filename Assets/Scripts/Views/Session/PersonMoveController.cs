using UnityEngine;

public class PersonMoveController : MonoBehaviour
{ 
    [SerializeField] private PTPersonComponent PersonObj;
    [SerializeField] private float sensitivity;
    [SerializeField] private Camera MainCamera;

    float resolution;
    private Vector3 newPos;
    private Vector3 currentPos;

    Vector3 distanceChange;
    Vector3 personPos;
    Vector3 checkFilterPos;

    private bool crackLeft;
    private bool crackRight;

    private int curTouchCount;

    public void Start()
    {
        curTouchCount = 0;
        resolution = (MainCamera.pixelHeight / (2 * MainCamera.orthographicSize));
    }

    public void SetTouchCount(int _touchCount)
    {
        curTouchCount = _touchCount;
    }

    public void PlayerMove(Vector3 touchPoint, int _touchCount)
    {
        if (curTouchCount != _touchCount)
        {
            curTouchCount = _touchCount;
            currentPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution, 0);
        }
        if (_touchCount == 1)
        {
            newPos = new Vector3(touchPoint.x / resolution, touchPoint.y / resolution, 0);
            if (Vector3.Distance(currentPos, newPos) > 0.01f)
            {
                distanceChange = (newPos - currentPos);
                personPos = PersonObj.transform.localPosition;
                checkFilterPos = personPos + distanceChange * sensitivity;
                distanceChange = CheckBorders(ref checkFilterPos, ref distanceChange);
                currentPos = newPos;
                PersonObj.Move(distanceChange * sensitivity);
            }
        }
    }

    public Vector3 CheckBorders(ref Vector3 _checkFilterPos, ref Vector3 _distanceChange)
    {
        if (_checkFilterPos.y < -5.5f || _checkFilterPos.y > 5.5f)
        {
            _distanceChange =  new Vector3(_distanceChange.x, 0, 0);
        } 
        if (_checkFilterPos.x > 2.5f)
        {
            crackRight = true;
            PersonObj.SetCrackRightActive(true);
            _distanceChange =  new Vector3(0, _distanceChange.y, 0);
        }
        if (_checkFilterPos.x < -2.5f)
        {
            crackLeft = true;
            PersonObj.SetCrackLeftActive(true);
            _distanceChange = new Vector3(0, _distanceChange.y, 0);
        }
        if(crackLeft) PersonObj.SetCrackLeftActive(false);
        if (crackRight) PersonObj.SetCrackRightActive(false);
        return _distanceChange;
    }

}