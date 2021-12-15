using System;
using Controlers;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.ChooseLevel;

namespace Core
{
    public class ChooseLevelCore : MonoBehaviour
    {
        public int CurrentLevelShowId;
        [SerializeField] private LevelPosPanelView LevelPosPanelViewObj;
        [SerializeField] private SessionLevelListScrObj SessionLevelListSO;
        [SerializeField] private LevelPanelListView LevelPanelListViewObj;

        [SerializeField] private InfoPanelView InfoPanelViewPb;
        [SerializeField] private Transform InfoPanelTarget;

        [SerializeField] private Text StorageCoins;
        public void Start()
        {
            SessionLevelListSO.Load();
            CurrentLevelShowId = SessionLevelListSO.CurrentSessionLevelId;
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            LevelPanelListViewObj.InitView(SessionLevelListSO, this);
            LevelPosPanelViewObj.InitView(SessionLevelListSO);
        }
        
        public void ShowNextLevel()
        {
            if (CurrentLevelShowId < SessionLevelListSO.List.Count - 1)
            {
                CurrentLevelShowId++;
                LevelPosPanelViewObj.UpdateView(CurrentLevelShowId);
                LevelPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
        }

        public void ShowPreviousLevel()
        {
            if (CurrentLevelShowId > 0)
            {
                CurrentLevelShowId--;
                LevelPosPanelViewObj.UpdateView(CurrentLevelShowId);
                LevelPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
        }
        public void BuyLevel()
        {
            if (CoinsControler.BuyEffect(SessionLevelListSO.List[CurrentLevelShowId].Cost))
            {
                SessionLevelControler.OpenLevel(CurrentLevelShowId);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
                
                LevelPosPanelViewObj.UpdateView(CurrentLevelShowId);
                LevelPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
            else
            {
                InfoPanelView newInfoPanel = Instantiate(InfoPanelViewPb, InfoPanelTarget);
                newInfoPanel.InitView("Cash", "You havent money");
            }
        }

        public void StartSession()
        {
            if (SessionLevelControler.LevelIsOpened(CurrentLevelShowId))
            {
                SessionLevelControler.SetCurrentLevel(CurrentLevelShowId);
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