using System;
using UnityEngine;

namespace Components.Session
{
    public class EnergyBarierComponent : MonoBehaviour
    {
       [SerializeField] private Transform Target;

        public void Update()
        {
            if (Target)
            {
                transform.localScale = new Vector3(0 + 2 - Vector3.Distance(Target.position,transform.position),0.5f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other);
            if (other.GetComponent<PTPersonComponent>())
            {
                Target = other.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PTPersonComponent>())
            {
                Target = null;
                transform.localScale = new Vector3(0.1f, 0.5f);
            }
        }
    }
}