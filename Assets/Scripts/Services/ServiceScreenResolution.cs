namespace Services
{
    using System;
    using UnityEngine;

    public class ServiceScreenResolution : MonoBehaviour
    {
        
        public static Vector3 GetScreenScale()
        {
            Vector3 result;
            float width = ScreenSize.GetScreenToWorldWidth;
            result = Vector3.one * width / 5.625f;
            return new Vector3((float)Math.Round(result.x, 2), (float)Math.Round(result.y, 2), (float)Math.Round(result.z, 2));
        }

        public static float GetScaledGameSpeed()
        {
            return 5 * (ScreenSize.GetScreenToWorldWidth / 5.625f);
        }
    }
}
