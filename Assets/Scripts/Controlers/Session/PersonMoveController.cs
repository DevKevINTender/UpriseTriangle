using UnityEngine;

public class PersonMoveController : MonoBehaviour
{ 
    [SerializeField] private PersonComponent PersonObj;
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
                currentPos = newPos;
                PersonObj.Move(distanceChange * sensitivity);
            }
        }
    }

}