using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    private int desCounter;
    // Start is called before the first frame update
    void Start()
    {
        InitComponent();
    }

    public void InitComponent()
    {
        transform.GetComponentInParent<PolygonCollider2D>().enabled = false;
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        List<ContactPoint2D> contact = new List<ContactPoint2D>();
        if (other.GetComponent<ObstacleComponent>())
        {
            desCounter++;
            Debug.Log(desCounter);
            Destroy(other.gameObject);
        }
    }
}
