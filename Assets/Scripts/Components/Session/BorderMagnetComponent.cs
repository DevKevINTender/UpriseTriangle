using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderMagnetComponent : MonoBehaviour
{
    public GameObject person = null;
    public int vector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (person != null)
        {
            switch (vector) // 0 left 1 up 2 right 3 down
            {
                case 0:
                {
                    person.transform.localPosition += new Vector3(Time.deltaTime,person.transform.localPosition.y);
                    break;
                } 
                case 1:
                {
                    person.transform.localPosition += new Vector3(person.transform.localPosition.x,Time.deltaTime);
                    break;
                }
                case 2:
                {
                    person.transform.localPosition += new Vector3(-Time.deltaTime,person.transform.localPosition.y);
                    break;
                }
                case 3:
                {
                    person.transform.localPosition += new Vector3(person.transform.localPosition.x,-Time.deltaTime);
                    break;
                }
            }
           
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test " +other.name);
        if (other.GetComponent<BonusCollectorComponent>())
        {
           
            person = other.transform.parent.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BonusCollectorComponent>())
        {
            person = null;
        }
    }
}
