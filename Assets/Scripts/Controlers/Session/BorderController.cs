using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private Transform top;
    [SerializeField] private Transform topper;

    [SerializeField] private Transform leftCoinBorder;
    [SerializeField] private Transform rightCoinBorder;
    [SerializeField] private Transform downCoinBorder;

    private float screenWidth;
    private float screenHeigth;


    void Start()
    {
        screenWidth = ScreenSize.GetScreenToWorldWidth / 2;
        screenHeigth = ScreenSize.GetScreenToWorldHeight / 2;

        SetPosition(ref top, 0, screenHeigth);
        SetPosition(ref topper, 0, screenHeigth + 0.2f);

        SetPosition(ref leftCoinBorder, -screenWidth, 0);
        SetPosition(ref rightCoinBorder, screenWidth, 0);
        Debug.Log(-screenHeigth);
        SetPosition(ref downCoinBorder, 0, -5f);
        
    }


    public void SetPosition(ref Transform _object, float _x, float _y)
    {
        _object.localPosition = new Vector3(_x, _y);
    }
}
