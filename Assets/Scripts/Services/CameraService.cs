using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    public Transform point;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goal = new Vector3(transform.position.x , point.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * 5);
    }
}
