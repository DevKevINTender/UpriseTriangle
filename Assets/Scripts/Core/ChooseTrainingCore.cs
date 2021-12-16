using Controlers;
using ScriptableObjects.SessionLevel;
using ScriptableObjects.TrainLevel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.ChooseLevel;
using Views.ChooseTraining;

namespace Core
{
    public class ChooseTrainingCore : MonoBehaviour
    {
        public int CurrentTrainigLevelShowId;
        [SerializeField] private TrainingPosPanelView TrainingPosPanelViewObj;
        [SerializeField] private TrainingLevelListScrObj TrainingLevelListSO;
        [SerializeField] private TrainingPanelListView TrainingPanelListViewObj;

        [SerializeField] private InfoPanelView InfoPanelViewPb;
        [SerializeField] private Transform InfoPanelTarget;

        [SerializeField] private Text StorageCoins;
        public void Start()
        {
            TrainingLevelListSO.Load();
            CurrentTrainigLevelShowId = TrainingLevelListSO.CurrentTrainigLevelId;
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            TrainingPanelListViewObj.InitView(TrainingLevelListSO, this);
            TrainingPosPanelViewObj.InitView(TrainingLevelListSO);
        }
        
        public void ShowNextLevel()
        {
            if (CurrentTrainigLevelShowId < TrainingLevelListSO.List.Count - 1)
            {
                CurrentTrainigLevelShowId++;
                TrainingPosPanelViewObj.UpdateView(CurrentTrainigLevelShowId);
                TrainingPanelListViewObj.UpdateView(CurrentTrainigLevelShowId);
            }
        }

        public void ShowPreviousLevel()
        {
            if (CurrentTrainigLevelShowId > 0)
            {
                CurrentTrainigLevelShowId--;
                TrainingPosPanelViewObj.UpdateView(CurrentTrainigLevelShowId);
                TrainingPanelListViewObj.UpdateView(CurrentTrainigLevelShowId);
            }
        }

        public void StartSession()
        {
            if (TrainingLevelControler.TrainingElementIsOpened(CurrentTrainigLevelShowId))
            {
                TrainingLevelControler.SetCurrentTraining(CurrentTrainigLevelShowId);
                SceneManager.LoadScene(1);
            }
            else
            {
                InfoPanelView newInfoPanel = Instantiate(InfoPanelViewPb, InfoPanelTarget);
                newInfoPanel.InitView("Exist", "What you try to do ? Am ?");
            }
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}