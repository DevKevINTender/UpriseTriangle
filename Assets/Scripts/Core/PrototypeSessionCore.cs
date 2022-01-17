using System.Collections;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

public class PrototypeSessionCore : MonoBehaviour
{
    [SerializeField] private PTSpawnBlockControler SpawnBlockControler;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private float startTime;
    [SerializeField] private GameObject PersonObj;

    [SerializeField] private float GameSpeed;
    void Start()
    {
        SpawnBlockControler.Init();
        AudioSource.time = startTime;
        PersonObj.transform.position = new Vector3(0,1 * startTime * GameSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
