using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    public float rotateSpeed;
    [SerializeField] bool reverse;
    void Start()
    {
        if (reverse) rotateSpeed = -rotateSpeed;
    }


    void Update()
    {
        
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }
}
