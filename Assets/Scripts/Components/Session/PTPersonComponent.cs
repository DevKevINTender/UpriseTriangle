using UnityEngine;

namespace Components.Session
{
    public class PTPersonComponent : MonoBehaviour
    {
        public void Move(Vector3 vector)
        {
            transform.position += vector;
        }
    }
}