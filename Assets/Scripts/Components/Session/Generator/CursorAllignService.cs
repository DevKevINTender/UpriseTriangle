using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAllignService : MonoBehaviour
{

    [SerializeField] private GameObject obsPb;
    [SerializeField] private GameObject player;
    [Header("Settings")]
    [SerializeField] private int obsCount;
    [Header("Time")]
    [SerializeField] private float time;
    [SerializeField] private float tempStart;
    [SerializeField] private float temp;

    private float width = 3.2f;
    private float height = 5.4f;
    private int side = 0;
    private float timeStart;
    private float timeStep;

    private GameObject obsObj;
    private CursorObstacleComponent cursor;
    [SerializeField] private ElevatorComponent elevator;

    public void Start()
    {
        height = height * (1 / transform.parent.parent.localScale.x);
        CreateObs();
    }

    public void TempToTiming()
    {
        timeStart = temp * (60f / 125f) * tempStart;
        timeStep = temp * (60f / 125f);
        obsCount = (int)((time - (60f / 125f) * 3) / timeStep);
        elevator.SetElevatorTime(obsCount * timeStep + (60f / 125f) * 3);
    }


    [ContextMenu("CreateObs")]
    public void CreateObs()
    {       
        DestroyChilds();
        TempToTiming();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            cursor = obsObj.GetComponent<CursorObstacleComponent>();
            cursor.startDelay = timeStart + timeStep * i;
            cursor.target = player;
            SetSpawnValues(obsObj);
        }
    }


    public void SetSpawnValues(GameObject _obsObj)
    {
        float spawnX = width - 1;
        float spawnY = height - 1;
        switch (side)
        {
            case 0:
                side += 2;
                _obsObj.transform.localPosition =  new Vector3(-width, Random.Range(-spawnY, spawnY), 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 0);               
                break;
            case 1:
                side++;
                _obsObj.transform.localPosition = new Vector3(Random.Range(-spawnX, spawnX), height, 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                side++;
                side = 0;
                _obsObj.transform.localPosition = new Vector3(width, Random.Range(-spawnY, spawnY), 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                side = 0;
                _obsObj.transform.localPosition = new Vector3(Random.Range(-spawnX, spawnX), -height, 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    public void DestroyChilds()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount; i > 0; --i)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}
