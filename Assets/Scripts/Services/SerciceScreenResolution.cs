using Services;
using UnityEngine;

public class SerciceScreenResolution : MonoBehaviour
{

    private void Awake()
    {
        float width = ScreenSize.GetScreenToWorldWidth;
        transform.localScale = Vector3.one * width / 5.625f;
    }
}
