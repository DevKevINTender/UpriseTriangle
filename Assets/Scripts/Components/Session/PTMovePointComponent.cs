using UnityEngine;

namespace Components.Session
{
    public class PTMovePointComponent : MonoBehaviour
    {
        
        public void Update()
        {
            transform.position += new Vector3(0,5*Time.deltaTime,0);
        }
    }
}