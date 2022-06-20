using UnityEngine;

[SelectionBase]
public class ObstacleComponent : MonoBehaviour
{
    public virtual void SelfDestroy()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
}
