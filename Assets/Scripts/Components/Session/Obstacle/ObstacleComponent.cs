using UnityEngine;

[SelectionBase]
public class ObstacleComponent : MonoBehaviour
{
    public virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
