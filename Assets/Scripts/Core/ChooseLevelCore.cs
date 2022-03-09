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
        [SerializeField] private LSPageView lsPageViewObj;
        [SerializeField] private SessionLevelListScrObj SessionLevelListSO;
        [SerializeField] private LSPanelListView lsPanelListViewObj;

        [SerializeField] private AlertPanelView alertPanelViewPb;
        [SerializeField] private Transform InfoPanelTarget;

        [SerializeField] private Text StorageCoins;
        public void Start()
        {
            SessionLevelListSO.Load();
            CurrentLevelShowId = SessionLevelListSO.CurrentSessionLevelId;
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            lsPanelListViewObj.InitView(SessionLevelListSO, this);
            lsPageViewObj.InitView(SessionLevelListSO);
        }
        
        public void ShowNextLevel()
        {
            if (CurrentLevelShowId < SessionLevelListSO.List.Count - 1)
            {
                CurrentLevelShowId++;
                lsPageViewObj.UpdateView(CurrentLevelShowId);
                lsPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
        }

        public void ShowPreviousLevel()
        {
            if (CurrentLevelShowId > 0)
            {
                CurrentLevelShowId--;
                lsPageViewObj.UpdateView(CurrentLevelShowId);
                lsPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
        }
        public void BuyLevel()
        {
            if (CoinsControler.BuyEffect(SessionLevelListSO.List[CurrentLevelShowId].Cost))
            {
                LevelChooseControler.OpenLevel(CurrentLevelShowId);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
                
                lsPageViewObj.UpdateView(CurrentLevelShowId);
                lsPanelListViewObj.UpdateView(CurrentLevelShowId);
            }
            else
            {
                AlertPanelView newAlertPanel = Instantiate(alertPanelViewPb, InfoPanelTarget);
                newAlertPanel.InitView("Cash", "You havent money");
            }
        }

        public void StartSession()
        {
            if (LevelChooseControler.LevelIsOpened(CurrentLevelShowId))
            {
                LevelChooseControler.SetCurrentLevel(CurrentLevelShowId);
                SceneManager.LoadScene(1);
            }
            else
            {
                AlertPanelView newAlertPanel = Instantiate(alertPanelViewPb, InfoPanelTarget);
                newAlertPanel.InitView("Exist", "What you try to do ? Am ?");
            }
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}