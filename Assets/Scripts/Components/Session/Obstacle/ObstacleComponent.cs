using UnityEngine;

[SelectionBase]
public class ObstacleComponent : MonoBehaviour
{
    internal virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
