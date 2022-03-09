using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [SerializeField] private float elevatorTime;
    [SerializeField] private SerciceScreenResolution serciceScreenResolution;
    private bool canMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] internal GameObject cursorAllignService;
    internal BoxTimeActivate BoxTimeActivate;
    private GameObject player;

    public void Start()
    {
        moveSpeed = serciceScreenResolution.GetScaledGameSpeed();
        canMove = false;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetElevatorTime(float time)
    {
        elevatorTime = time;
    }

    public float GetElevatorTime()
    {
        return elevatorTime;
    }

    public void Update()
    {
        if(canMove)
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MovePointComponent>())
        {
            ActivateElevator();
            player = other.gameObject;
            player.transform.GetChild(0).GetComponent<PersonComponent>().inElevator = true;
        }
    }

    private void ActivateElevator()
    {
        StartCoroutine(ElevatorTimeCur());
        if(BoxTimeActivate != null) BoxTimeActivate.StartAction();
        if (cursorAllignService != null) cursorAllignService.SetActive(true);
    }

    private IEnumerator ElevatorTimeCur()
    {
        canMove = true;
        yield return new WaitForSeconds(elevatorTime);
        StopElevator();
    }

    private void StopElevator()
    {
        player.transform.GetChild(0).GetComponent<PersonComponent>().inElevator = false;
        canMove = false;
    }
}