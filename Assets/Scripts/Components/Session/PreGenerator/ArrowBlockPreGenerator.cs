using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ArrowBlockPreGenerator : MonoBehaviour
{

    [SerializeField] private GameObject obsPb;
    [SerializeField] private GameObject player;
    [Header("Settings")]
    [SerializeField] private int obsCount;
    [SerializeField] private float spawnRound;
    [Range(-1, 1)]
    [SerializeField] private int sides;
    [Header("Time")]
    [SerializeField] private float time;
    [SerializeField] private float tempStart;
    [SerializeField] private float temp;

    private float width;
    private float height;
    private float scaleHeight;
    private int side = 0;
    private float timeStart;
    private float timeStep;

    private GameObject obsObj;
    private ArrowObstacleComponent cursor;
    [SerializeField] private ElevatorComponent elevator;

    public void Start()
    {
        MoveAtStart();
    }

    public void StartValues()
    {
        width = 3.2f;
        height = 5.4f;
        scaleHeight = height * (1 / transform.parent.parent.localScale.x);
    }

    public void MoveAtStart()
    {
        StartValues();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.localPosition.y == height)
            {
                child.localPosition = new Vector3(child.localPosition.x, scaleHeight, 0);
            }
            if (child.localPosition.y == -height)
            {
                child.localPosition = new Vector3(child.localPosition.x, -scaleHeight, 0);
            }
        }
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
        if (sides == -1) side = 1;
        StartValues();
        TempToTiming();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = PrefabUtility.InstantiatePrefab(obsPb, transform) as GameObject;
            cursor = obsObj.GetComponent<ArrowObstacleComponent>();
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
                side += 1 + sides;
                _obsObj.transform.localPosition =  new Vector3(-width, Round(Random.Range(-spawnY, spawnY), spawnRound), 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 0);               
                break;
            case 1:
                if (sides == -1) side++;
                side++;
                _obsObj.transform.localPosition = new Vector3(Round(Random.Range(-spawnX, spawnX), spawnRound), height, 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                side++;
                _obsObj.transform.localPosition = new Vector3(width, Round(Random.Range(-spawnY, spawnY), spawnRound), 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 180);
                if (sides == 1) side = 0;
                break;
            case 3:
                side = 0;
                _obsObj.transform.localPosition = new Vector3(Round(Random.Range(-spawnX, spawnX), spawnRound), -height, 0);
                obsObj.transform.rotation = Quaternion.Euler(0, 0, 90);
                if (sides == -1) side = 1;
                break;
        }
    }

    public float Round(float num, float fraction)
    {
        return Mathf.Round(num / fraction) * fraction;
    }

    public void DestroyChilds()
    {
        side = 0;
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount; i > 0; --i)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}
