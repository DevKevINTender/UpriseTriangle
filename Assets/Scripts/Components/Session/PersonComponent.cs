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
    [SerializeField] private PersonParticleController personParticleController;

    float timer;

    private float vectroToRotate;
    private float targetvectroToRotate;

    private float maxX;
    private float minX;

    private float maxY;
    private float minY;


    
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void EnterElevator()
    {
        personParticleController.StopParticle();
    }

    public void ExitElevator()
    {
        personParticleController.StartParticle();
    }

    public void InitComponent(PersonDelegate personDeathTrigger, PersonDelegate personWinTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
        this.personWinTrigger = personWinTrigger;

        maxX = 2.6f * ServiceScreenResolution.GetScreenScale().x;
        minX = 2.45f * ServiceScreenResolution.GetScreenScale().x;
            
        maxY = 6.05f * ServiceScreenResolution.GetScreenScale().y;
        minY= 5.9f * ServiceScreenResolution.GetScreenScale().y;
    }

    public void Move(Vector3 vector)
    {
        if (canMove)
        {
            vectroToRotate = vector.normalized.x * -15;
            transform.localPosition += vector;
        }     
    }

    private void CheckTeleport()
    {
       
        float distanceX = Math.Abs(maxX - minX);
        float stepX = distanceX / 100;
        float personDistX = maxX - Mathf.Abs(transform.localPosition.x);
            
        float distanceY = Math.Abs(maxY - minY);
        float stepY = distanceY / 100;
        float personDistY = maxY - Mathf.Abs(transform.localPosition.y);
        
        // условия изменения по скейлу
        if(Mathf.Abs(transform.localPosition.x) > minX && Mathf.Abs(transform.localPosition.x) < maxX)
            transform.localScale = Vector3.one * ( (personDistX / stepX) / 100);

        if(Mathf.Abs(transform.localPosition.y) > minY && Mathf.Abs(transform.localPosition.y) < maxY)
            transform.localScale = Vector3.one * ( (personDistY / stepY) / 100);

        if (Mathf.Abs(transform.localPosition.y) < minY && Mathf.Abs(transform.localPosition.x) < minX)
            transform.localScale = Vector3.one;

        // телепортация по оси X
        if (transform.localPosition.x > maxX)
            transform.localPosition = new Vector3(-maxX + 0.5f , transform.localPosition.y);
        if (transform.localPosition.x < -maxX)
            transform.localPosition = new Vector3(maxX - 0.5f, transform.localPosition.y);

        // телепортация по оси Y    
        if (transform.localPosition.y > maxY)
            transform.localPosition = new Vector3(transform.localPosition.x, -maxY + 0.5f);
        if (transform.localPosition.y < -maxY)
            transform.localPosition = new Vector3(transform.localPosition.x, maxY - 0.5f);

        float speedMove = Time.deltaTime / 2;
        if (transform.localPosition.x > minX)
            transform.localPosition += new Vector3(speedMove, 0);
        if (transform.localPosition.x < -minX)
            transform.localPosition += new Vector3(-speedMove, 0);
        // телепортация по оси Y    
        if (transform.localPosition.y > minY)
            transform.localPosition += new Vector3(0, speedMove);
        if (transform.localPosition.y < -minY)
            transform.localPosition += new Vector3(0, -speedMove);
    }

    public void Update()
    {
        vectroToRotate = Mathf.MoveTowards(vectroToRotate, 0, Time.deltaTime * 60);
        targetvectroToRotate = Mathf.MoveTowards(targetvectroToRotate, vectroToRotate, Time.deltaTime * 60);
        transform.rotation = Quaternion.Euler( 0,0, targetvectroToRotate);
        CheckTeleport();      
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