using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.TrainLevel;
using UnityEngine;

namespace Components.Session
{
    public class ElevatorComponent : MonoBehaviour
    {
        [SerializeField] private float elevatorTime;
        [SerializeField] private Transform spectaclePoint; //точка слежения лифта // это та точка в пространсве, за которой едет лифт
        [SerializeField] private List<PTWayObsComponent> laserList = new List<PTWayObsComponent>();
        private bool canMove;

        public void Start()
        {
            canMove = false;
            spectaclePoint = transform;
        }


        public void Update()
        {
            if(canMove)
            transform.position += new Vector3(0, 4.1f * Time.deltaTime, 0);
        }
    
        public void AddToList(PTWayObsComponent _pTWayObsComponent)
        {
            laserList.Add(_pTWayObsComponent);
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PTMovePointComponent>())
            {
                spectaclePoint = other.transform;
                ActivateElevator();
            }
        }

        private void ActivateElevator()
        {
            StartCoroutine(ElevatorTimeCur());
            //Activate Laser
            foreach (var item in laserList)
            {
                item.ObstacleInit();
            }           
        }

        private IEnumerator ElevatorTimeCur()
        {
            canMove = true;
            yield return new WaitForSecondsRealtime(elevatorTime);
            StopElevator();
        }

        private void StopElevator()
        {
            canMove = false;
            spectaclePoint = transform;
        }
    }
}