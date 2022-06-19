using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SkillSO", menuName = "ScrObj/new SkillSO", order = 0)]
    public class SkillScrObj : ScriptableObject
    {
        public int Id;
        public int CurrentSegment;
        public int RequiredSegments;
        public SkillType skillType;
        public float skillValue;
        public string skillDescription;
        public enum SkillType
        {
            LevelCompleteIncreaseCoin,
            AddCoinIfCollectCoin,
            AddSegmentWhenCollectCoin,
            ShieldProtect,
        }
    }
}