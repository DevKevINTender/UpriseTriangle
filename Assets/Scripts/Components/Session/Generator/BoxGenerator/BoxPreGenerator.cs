using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEditor;


public class BoxPreGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obsPb;
    [Header("Settings")]
    [SerializeField] private float countX;
    [SerializeField] private float countY;

    private GameObject obsObj;
    private float objScaleX;
    private float objScaleY;

    private float screenScale;
    private float screenSizeX;
    private float screenSizeY = 10;

    private float maxX;
    private float maxY;

    private Vector3 startPos;
    private Vector3 currentPos;

    public void Start()
    {        
        SetStartValues();
    }

    public void SetStartValues()
    {
        screenScale = transform.parent.parent.transform.localScale.x;
        screenSizeX = ScreenSize.GetScreenToWorldWidth;

        objScaleX = screenSizeX / countX;
        objScaleY = screenSizeY / countY;

        maxX = (screenSizeX / 2 - 0.5f * objScaleX) * (1 / screenScale);
        maxY = (screenSizeY / 2 - 0.5f * objScaleY) * (1 / screenScale);
        startPos = new Vector3(-maxX, maxY);
    }
   
    public void Spawn(int i, int j)
    {
        obsObj = Instantiate(obsPb, transform) as GameObject;
        obsObj.transform.localScale = new Vector3(objScaleX, objScaleY) / screenScale;
        currentPos = startPos + new Vector3(obsObj.transform.localScale.x * i, 0);
        currentPos -= new Vector3(0, obsObj.transform.localScale.y * j);
        obsObj.transform.localPosition = currentPos;
    }

    public float GetBoxTime()
    {
       return obsPb.GetComponent<BoxObstacleComponent>().GetBoxTime();
    }
}
