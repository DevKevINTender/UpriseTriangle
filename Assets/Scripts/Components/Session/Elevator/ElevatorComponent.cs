using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [SerializeField] private float elevatorTime;
    private bool canMove;
    [SerializeField] private float gameSpeed;
    [SerializeField] internal List<ElevatorMarkComponent> elevatorUnits;
    private GameObject player;

    public void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponent<ElevatorMarkComponent>())
            elevatorUnits.Add(child.GetComponent<ElevatorMarkComponent>());
        }
        gameSpeed = ServiceScreenResolution.GetScaledGameSpeed();
        canMove = false;
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
        transform.position += new Vector3(0, gameSpeed * Time.deltaTime, 0);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MovePointComponent>())
        {
            ActivateElevator();
            player = other.gameObject;
            player.transform.GetChild(0).GetComponent<PersonComponent>().EnterElevator();
        }
    }

    private void ActivateElevator()
    {
        StartCoroutine(ElevatorTimeCur());
        foreach (ElevatorMarkComponent elevatorUnit in elevatorUnits)
        {
            elevatorUnit.StartAction();
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
        player.transform.GetChild(0).GetComponent<PersonComponent>().ExitElevator();
        canMove = false;
    }
}