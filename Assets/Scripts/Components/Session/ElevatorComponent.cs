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
        [SerializeField] private List<PTWayObsComponent> laserList = new List<PTWayObsComponent>();
        [SerializeField] private SerciceScreenResolution serciceScreenResolution;
        private bool canMove;
        [SerializeField] private float moveSpeed;

        public void Start()
        {
            moveSpeed = serciceScreenResolution.GetScaledGameSpeed();
            canMove = false;
        }


        public void Update()
        {
            if(canMove)
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    
        public void AddToList(PTWayObsComponent _pTWayObsComponent)
        {
            laserList.Add(_pTWayObsComponent);
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PTMovePointComponent>())
            {
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
            yield return new WaitForSeconds(elevatorTime);
            StopElevator();
        }

        private void StopElevator()
        {
            canMove = false;
        }
    }
}