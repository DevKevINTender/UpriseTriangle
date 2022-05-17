using System.Collections;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class CoinBombController : ElevatorMarkComponent
{
    [SerializeField] private GameObject coinBombPb;
    [SerializeField] private float spawnRate;
    private GameObject coinBombObj;
    private float spawnCount;

    private float screenWidth;
    private float screenHeigth;

    private float lifeTime;
    private Vector3 spawnPoint;
    private float x;
    private float y;

    private void Start()
    {
        lifeTime = transform.parent.transform.GetComponent<ElevatorComponent>().GetElevatorTime();
        spawnCount = lifeTime / spawnRate;
        screenWidth = ScreenSize.GetScreenToWorldWidth / 2 - 1;
        screenHeigth = 1.5f;
    }

    internal override void StartAction()
    {
        if(spawnCount > 1) //чтобы не было спавна на последней секунде лифта
            StartCoroutine(SpawnDelay());

    }


    private IEnumerator SpawnDelay()
    {
        x = Random.Range(-screenWidth, screenWidth);
        y = Random.Range(-screenHeigth, screenHeigth);
        spawnPoint = new Vector3(x, y);
        coinBombObj = Instantiate(coinBombPb, transform);
        coinBombObj.transform.localPosition = spawnPoint;
        spawnCount--;
        yield return new WaitForSeconds(spawnRate);
        StartAction();
    }

}
