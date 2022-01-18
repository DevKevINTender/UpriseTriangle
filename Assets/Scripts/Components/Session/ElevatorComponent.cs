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
        private int laserOrder;

        public void Start()
        {
            spectaclePoint = transform;
        }
        public void Update()
        {
            transform.position = spectaclePoint.position;
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
            yield return new WaitForSecondsRealtime(elevatorTime);
            StopElevator();
        }
        private void StopElevator()
        {
            spectaclePoint = transform;
        }
    }
}