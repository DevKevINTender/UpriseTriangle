﻿using System;
using System.Collections;
using Controlers;
using ScriptableObjects;
using Services;
using Unity.Mathematics;
using UnityEngine;

public class PersonComponent : MonoBehaviour
{
    public delegate void PersonDelegate();
    private PersonDelegate personDeathTrigger;
    private PersonDelegate personWinTrigger;
    private PersonDelegate personEnterElevatorTrigger;
    private PersonDelegate personExitElevatorTrigger;

    private bool canMove;
    private bool isMove;
    internal bool inElevator;
    [SerializeField] private PersonParticleController personParticleController;
    [SerializeField] private PersonFinishComponent PersonFinishComponent;
    float timer;

    private float vectroToRotate;
    private float targetvectroToRotate;

    private float maxX;
    private float minX;

    private float maxY;
    private float minY;

    [SerializeField] private SpriteRenderer personSkin;
    [SerializeField] private ParticleSystem effectSprite;
    
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void EnterElevator()
    {
        inElevator = true;
        personParticleController.StopParticle();
        personEnterElevatorTrigger?.Invoke();
    }

    public void ExitElevator()
    {
        inElevator = false;
        personParticleController.StartParticle();
        personExitElevatorTrigger?.Invoke();
    }

    public void InitComponent(PersonDelegate personDeathTrigger, PersonDelegate personWinTrigger,
        PersonDelegate  personEnterElevatorTrigger, PersonDelegate personExitElevatorTrigger)
    {
        this.personDeathTrigger = personDeathTrigger;
        this.personWinTrigger = personWinTrigger;
        this.personEnterElevatorTrigger = personEnterElevatorTrigger;
        this.personExitElevatorTrigger = personExitElevatorTrigger;

        PersonFinishComponent.InitComponent(personWinTrigger);

        maxX = (ScreenSize.GetScreenToWorldWidth / 2) - 0.25f;
        minX = 2.45f * ServiceScreenResolution.GetScreenScale().x;
            
        maxY = (ScreenSize.GetScreenToWorldHeight / 2) - 0.25f;
        minY= 4.75f * ServiceScreenResolution.GetScreenScale().y;

        PersonScrObj personInfo = PersonStorageContoler.GetPersonById(PersonStorageContoler.GetCurrentPerson());

        personSkin.sprite = personInfo.PersonSkin;
        effectSprite.textureSheetAnimation.SetSprite(0,personInfo.Effect);
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

    private void CheckBarier()
    {
        float distanceX = Math.Abs(maxX - minX);
        float stepX = distanceX / 100;
        float personDistX = maxX - Mathf.Abs(transform.localPosition.x);
            
        float distanceY = Math.Abs(maxY - minY);
        float stepY = distanceY / 100;
        float personDistY = maxY - Mathf.Abs(transform.localPosition.y);
        
        if (transform.localPosition.x > maxX)
            transform.localPosition = new Vector3(maxX , transform.localPosition.y);
        if (transform.localPosition.x < -maxX)
            transform.localPosition = new Vector3(-maxX , transform.localPosition.y);
        
        if (transform.localPosition.y > maxY)
            transform.localPosition = new Vector3(transform.localPosition.x, maxY);
        if (transform.localPosition.y < -maxY)
            transform.localPosition = new Vector3(transform.localPosition.x, -maxY);
    }
   
    public void Update()
    {
        vectroToRotate = Mathf.MoveTowards(vectroToRotate, 0, Time.deltaTime * 60);
        targetvectroToRotate = Mathf.MoveTowards(targetvectroToRotate, vectroToRotate, Time.deltaTime * 60);
        transform.rotation = Quaternion.Euler( 0,0, targetvectroToRotate);
        //CheckTeleport();
        CheckBarier();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleComponent>())
        {
            personDeathTrigger?.Invoke();
        }
       
    }
}