using UnityEngine;
using Services;

public class LevelScaleService : MonoBehaviour
{

    void Awake()
    {
        transform.localScale = ServiceScreenResolution.GetScreenScale();
    }

}
