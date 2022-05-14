using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [SerializeField] private float elevatorTime;
    private bool canMove;
    [SerializeField] private float gameSpeed;
    [SerializeField] internal GameObject cursorAllignService;
    private CoinDispenserController coinDispenserController;
    private CoinBombController coinBombController;
    internal BoxTimeActivate BoxTimeActivate;
    private GameObject player;

    public void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<CoinDispenserController>())
                coinDispenserController = transform.GetChild(0).GetComponent<CoinDispenserController>();
            if (child.GetComponent<CoinBombController>())
                coinBombController = transform.GetChild(0).GetComponent<CoinBombController>();
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
            player.transform.GetChild(0).GetComponent<PersonComponent>().inElevator = true;
        }
    }

    private void ActivateElevator()
    {
        StartCoroutine(ElevatorTimeCur());
        if(BoxTimeActivate != null) BoxTimeActivate.StartAction();
        if(cursorAllignService != null) cursorAllignService.SetActive(true);
        if(coinDispenserController != null) coinDispenserController.StartAction();
        if(coinBombController != null) coinBombController.StartAction();
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