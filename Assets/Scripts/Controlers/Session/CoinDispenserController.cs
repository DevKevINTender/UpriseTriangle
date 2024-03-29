using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDispenserController : ElevatorMarkComponent
{
    [SerializeField] private List<CoinDispenserComponent> coinDispenserComponents;
    [SerializeField] private float coinsCount;
    [SerializeField] private float spawnTime;

    private float timeDelay;

    internal override void StartAction()
    {
        StartCoroutine(StartSpawn());
    }


    private IEnumerator StartSpawn()
    {
        timeDelay = spawnTime / (coinsCount / coinDispenserComponents.Count);
        while(coinsCount > 0)
        {
            foreach (CoinDispenserComponent CoinDispenser in coinDispenserComponents)
            { 
                CoinDispenser.SpawnCoin();
                coinsCount--;
            }
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
