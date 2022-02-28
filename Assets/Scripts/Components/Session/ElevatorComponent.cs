using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.TrainLevel;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [SerializeField] internal float elevatorTime;
    [SerializeField] private List<PTWayObsComponent> laserList = new List<PTWayObsComponent>();
    [SerializeField] private SerciceScreenResolution serciceScreenResolution;
    private bool canMove;
    [SerializeField] private float moveSpeed;
    private List<CursorAllignService> cursorAllignList = new List<CursorAllignService>();
    internal SquareTimeService squareTimeService;

    public void Start()
    {
        moveSpeed = serciceScreenResolution.GetScaledGameSpeed();
        canMove = false;
    }


    public void Update()
    {
        if(canMove)
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
    
    public void AddToList(PTWayObsComponent _pTWayObsComponent)
    {
        laserList.Add(_pTWayObsComponent);
    }

    public void AddToList(CursorAllignService _cursorAllignService)
    {
        cursorAllignList.Add(_cursorAllignService);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PTMovePointComponent>())
        {
            ActivateElevator();
        }
    }

    private void ActivateElevator()
    {
        StartCoroutine(ElevatorTimeCur());
        squareTimeService.StartAction();
        foreach (var item in cursorAllignList) item.ActivateCursors();
        //Activate Laser
        foreach (var item in laserList)
        {
            item.ObstacleInit();
        }           
    }

    private IEnumerator ElevatorTimeCur()
    {
        canMove = true;
        yield return new WaitForSeconds(elevatorTime);
        StopElevator();
    }

    private void StopElevator()
    {
        canMove = false;
    }
}