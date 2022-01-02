using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 15 & GetComponent<EdgeCollider2D>() != null)
        {
            GetComponent<EdgeCollider2D>().enabled = true;
        }
        transform.position += new Vector3(0,-moveSpeed * Time.deltaTime,0);
    }
}
