using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorErrorTextComponent : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private float speed;
    [Range(-1, 1)]
    [SerializeField] int side;
    private float borderSide;


    void Start()
    {
        borderSide = 730;
    }

    public void Update()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.localPosition += Vector3.right * Time.deltaTime * 100 * speed * side;
        if (side == -1)
        {
            if (rectTransform.localPosition.x <= -borderSide)
                rectTransform.localPosition = new Vector3(borderSide, 0);
        }
        else
        {
            if (rectTransform.localPosition.x >= borderSide)
                rectTransform.localPosition = new Vector3(-borderSide, 0);
        }
    }

}
