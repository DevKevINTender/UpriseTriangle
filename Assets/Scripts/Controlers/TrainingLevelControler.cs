using ScriptableObjects.TrainLevel;
using UnityEngine;

namespace Controlers
{
    public class TrainingLevelControler
    {
        private static TrainingLevelListScrObj TrainingLevelListSO = Resources.Load<TrainingLevelListScrObj>("ScriptableObjects/TrainLevel/TrainingLevelListSO");
        
        public static bool TrainingElementIsOpened(int Id)
        {
            TrainingLevelListSO.Load();
            
            foreach (var item in TrainingLevelListSO.OpenedTrainingLevelIdList)
            {
                if (Id == item) return true;
            }

            return false;
        }

        public static void OpenTrainingElement(int Id)
        {
            TrainingLevelListSO.OpenedTrainingLevelIdList.Add(Id);
            TrainingLevelListSO.Save();
        }

        public static void SetCurrentTraining(int Id)
        {
            TrainingLevelListSO.CurrentTrainigLevelId = Id;
            TrainingLevelListSO.Save();
        }
    }
}