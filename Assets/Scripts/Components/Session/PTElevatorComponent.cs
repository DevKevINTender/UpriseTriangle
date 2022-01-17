using System;
using UnityEngine;

namespace Components.Session
{
    public class PTElevatorComponent : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerPB;
        [SerializeField] private float ElevatorTime;
        public void InitComponent(GameObject playerPB)
        {
            this.PlayerPB = playerPB;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            
        }
    }
}