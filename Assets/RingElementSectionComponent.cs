using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingElementSectionComponent : MonoBehaviour
{
    float bulletSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame

    public void SetSpeed(float _bulletSpeed)
    {
        bulletSpeed = _bulletSpeed;
    }
    
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }
}
