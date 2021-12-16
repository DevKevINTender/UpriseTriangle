using UnityEngine;

namespace ScriptableObjects.TrainLevel
{
    [CreateAssetMenu(fileName = "TrainingLevelSO", menuName = "ScrObj/new TrainingLevelSO", order = 0)]
    public class TrainingLevelScrObj : ScriptableObject
    {
        public int LevelId;
        public string ElementName;
        public string ElementSlogan;

        public int DeadCount;
        public int TrainingCount;
        public int CompletePercent;
    }
}