using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EffectSO", menuName = "ScrObj/new EffectSO", order = 0)]
    public class EffectScrObj : ScriptableObject
    {
        public int Id;
        public GameObject EffectPb;
    }
}