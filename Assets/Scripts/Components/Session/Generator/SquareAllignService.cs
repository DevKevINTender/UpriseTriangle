using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class SquareAllignService : MonoBehaviour
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
        Spawn();
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

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        DestroyChilds();
        SetStartValues();
        for (int j = 0; j < countY; j++)
        { 
            for (int i = 0; i < countX; i++)
            {
                obsObj = Instantiate(obsPb, transform);
                obsObj.transform.localScale = new Vector3(objScaleX, objScaleY) / screenScale;
                currentPos = startPos + new Vector3((obsObj.transform.localScale.x * i), 0);
                obsObj.transform.localPosition = currentPos;
                Debug.Log(obsObj.transform.localScale);
            }
            startPos -= new Vector3(0, (obsObj.transform.localScale.y));
        }
    }


    public void DestroyChilds()
    {
        if (transform.childCount > 0)
            for (int i = transform.childCount; i > 0; --i)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
    }
}
