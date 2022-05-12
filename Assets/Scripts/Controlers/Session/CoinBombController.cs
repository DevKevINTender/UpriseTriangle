using System.Collections;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class CoinBombController : MonoBehaviour
{
    [SerializeField] private GameObject coinBombPb;
    [SerializeField] private float spawnRate;

    private float screenWidth;
    private float screenHeigth;
    private float lifeTime;

    private void Start()
    {
        lifeTime = transform.parent.transform.GetComponent<ElevatorComponent>().GetElevatorTime();
        screenWidth = ScreenSize.GetScreenToWorldWidth / 2;
        screenHeigth = ScreenSize.GetScreenToWorldHeight / 2;
    }

    public void StartAction()
    {
        StartCoroutine(SpawnDelay());
    }


    private IEnumerator SpawnDelay()
    {

        Instantiate(coinBombPb, transform);
        yield return new WaitForSeconds(spawnRate);
    }

}
