using System;
using System.Collections;
using Services;
using Unity.Mathematics;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    public delegate void PersonDelegate();
    private PersonDelegate personDeathTrigger;
    private PersonDelegate personWinTrigger;

    private bool canMove;
    private bool isMove;
    internal bool inElevator;
    [SerializeField] private Rigidbody2D personRb;
    [SerializeField] private float moveToCenterTime;
    [SerializeField] private GameObject crackLeft;
    [SerializeField] private GameObject crackRight;

    float timer;

    private float vectroToRotate;
    private float targetvectroToRotate;


    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
 
    public void InitComponent(PersonDelegate personDeathTrigger, PersonDelegate personWinTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
        this.personWinTrigger = personWinTrigger;
    }

    public void Move(Vector3 vector)
    {
        if (canMove)
        {
            vectroToRotate = vector.normalized.x * -15;
            transform.position += vector;

            Teleport();
        }
       
    }

    private void Teleport()
    {
        float maxX = 2.65f * ServiceScreenResolution.GetScreenScale().x;
        float minX = 2.5f * ServiceScreenResolution.GetScreenScale().x;
            
        float maxY = 6.05f * ServiceScreenResolution.GetScreenScale().y;
        float minY= 5.9f * ServiceScreenResolution.GetScreenScale().y;
            
        float distanceX = Math.Abs(maxX - minX);
        float stepX = distanceX / 100;
        float personDistX = maxX - Mathf.Abs(transform.localPosition.x);
            
        float distanceY = Math.Abs(maxY - minY);
        float stepY = distanceY / 100;
        float personDistY = maxY - Mathf.Abs(transform.localPosition.y);
        
        // условия изменения по скейлу
        if(Mathf.Abs(transform.localPosition.x) > minX && Mathf.Abs(transform.localPosition.x) < maxX)
        {
            transform.localScale = new Vector3(1,1,1) * ( (personDistX/stepX) / 100);
        }

        if(Mathf.Abs(transform.localPosition.y) > minY && Mathf.Abs(transform.localPosition.y) < maxY)
        {
            transform.localScale = new Vector3(1,1,1) * ( (personDistY/stepY) / 100);
        }
            
        if(Mathf.Abs(transform.localPosition.y) < minY & Mathf.Abs(transform.localPosition.x) < minX)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        // телепортация по оси X
        if (transform.localPosition.x > maxX)
        {
            transform.localPosition = new Vector3(-maxX , transform.localPosition.y,0);
        }
        if (transform.localPosition.x < -maxX)
        {
            transform.localPosition = new Vector3(maxX , transform.localPosition.y,0);
        }
        // телепортация по оси Y    
        if (transform.localPosition.y > maxY)
        {
            transform.localPosition = new Vector3( transform.localPosition.x,-maxY  ,0);
        }
        if (transform.localPosition.y < -maxY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,maxY ,0);
        }
    }
    public void Update()
    {
        vectroToRotate = Mathf.MoveTowards(vectroToRotate, 0, Time.deltaTime * 60);
        targetvectroToRotate = Mathf.MoveTowards(targetvectroToRotate, vectroToRotate, Time.deltaTime * 60);
        transform.rotation = Quaternion.Euler( 0,0, targetvectroToRotate); 
        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            personDeathTrigger?.Invoke();
        }
        if (other.GetComponent<FinishLineComponent>())
        {
            personWinTrigger?.Invoke();
        }
    }
}