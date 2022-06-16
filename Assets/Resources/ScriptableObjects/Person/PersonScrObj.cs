using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PersonSO", menuName = "ScrObj/new PersonSO", order = 0)]
    public class PersonScrObj : ScriptableObject
    {
        public int Id;
        public int CurrentSegment;
        public int RequiredSegments;
        public Sprite Effect;
        public Sprite PersonSkin;
        public Sprite PersonShield;
    }
}