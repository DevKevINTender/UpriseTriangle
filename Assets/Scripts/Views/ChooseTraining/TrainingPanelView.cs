using ScriptableObjects.TrainLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseTraining
{
    public class TrainingPanelView : MonoBehaviour
    {
        private TrainingLevelScrObj TrainingLevelSO;

        public Text Level;
        public Text ElementName;
        public Text ElementSlogan;

        public Text DeadCount;
        public Text TrainingCount;
        public Text CompletePercent;
        
        
        public void InitView(TrainingLevelScrObj TrainingLevelSO)
        {
            this.TrainingLevelSO = TrainingLevelSO;

            Level.text = $"{TrainingLevelSO.LevelId + 1}";
            ElementName.text = $"{TrainingLevelSO.ElementName}";
            ElementSlogan.text = $"{TrainingLevelSO.ElementSlogan}";

            DeadCount.text = $"{TrainingLevelSO.DeadCount}";
            TrainingCount.text = $"{TrainingLevelSO.TrainingCount}";
            CompletePercent.text = $"{TrainingLevelSO.CompletePercent}";
        }
    }
}