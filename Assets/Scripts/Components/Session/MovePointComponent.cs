using UnityEngine;
using Services;

public class MovePointComponent : MonoBehaviour
{
    private float gameSpeed;

    public void Start()
    {
        SetGameSpeed();
    }

    public void SetGameSpeed()
    {
        gameSpeed = ServiceScreenResolution.GetScaledGameSpeed();
    }

    public void Update()
    {
        transform.position += new Vector3(0, gameSpeed * Time.deltaTime, 0);
    }

    // перемещение игрока к заданной точке
    public void TimeTransfer(float _startTime, float _gameSpeed)
    {
        transform.position = new Vector3(0, _startTime * _gameSpeed);
    }
}