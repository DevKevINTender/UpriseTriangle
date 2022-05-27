using System;
using Controlers;
using DG.Tweening;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.ChooseLevel;

namespace Core
{
    public  delegate  void LSItemAction(int id);
    public  delegate  void LSPageAction();
    public class ChooseLevelCore : MonoBehaviour
    {
        [SerializeField] private LevelChooseAudioControler LevelChooseAudioControlerObj;
        
        [SerializeField] public Transform canvas;

        [SerializeField] public SessionLevelListScrObj SessionLevelListSO;
        [SerializeField] public Text StorageCoins;
        [SerializeField] public LevelChoosePageView levelChoosePageViewPb;
        [SerializeField] public LevelChoosePageView levelChoosePageViewCurrentObj;

        [SerializeField] public LSPanelView LSPanelViewObj;
        [SerializeField] private AlertPanelView alertPanelPb;
        [SerializeField] private Transform infoPanelPos;

        [SerializeField] private PageIndicatorPanelView PageIndicatorPanelViewObj;

        public int CurrentLevelShowId;
        public double CurrentPageId;
        public double PageCount;

        public void Start()
        {
            CoinsControler.UpcreaseCoins(10000);
            
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            
            SessionLevelListSO.Load();
            CurrentLevelShowId = SessionLevelListSO.CurrentSessionLevelId;
            
            CurrentPageId =  Math.Floor((double) CurrentLevelShowId / 3);
            PageCount = Math.Ceiling((double) SessionLevelListSO.List.Count / 3);
            
            levelChoosePageViewCurrentObj = Instantiate(levelChoosePageViewPb,canvas);
            
            levelChoosePageViewCurrentObj.InitView(LevelChooseControler.GetSessionLevelsFromPage((int)CurrentPageId),BuyLevel, ChooseLevel, ShowNextPage, ShowPreviousPage);
            LSPanelViewObj.InitView(this);
            PageIndicatorPanelViewObj.InitView((int)PageCount);
            PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
        }
        
        public void ShowNextPage()
        {
            if (CurrentPageId < PageCount-1)
            {
                CurrentPageId++;
                LevelChoosePageView previousPage = levelChoosePageViewCurrentObj;
                levelChoosePageViewCurrentObj = Instantiate(levelChoosePageViewPb,canvas);
                levelChoosePageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1500,0);
                levelChoosePageViewCurrentObj.InitView(LevelChooseControler.GetSessionLevelsFromPage((int)CurrentPageId),BuyLevel, ChooseLevel, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                levelChoosePageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                
                PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
            }
        }

        public void ShowPreviousPage()
        {
            if (CurrentPageId > 0)
            {
                CurrentPageId--;
                LevelChoosePageView previousPage = levelChoosePageViewCurrentObj;
                levelChoosePageViewCurrentObj = Instantiate(levelChoosePageViewPb,canvas);
                levelChoosePageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1500,0);
                levelChoosePageViewCurrentObj.InitView(LevelChooseControler.GetSessionLevelsFromPage((int)CurrentPageId),BuyLevel, ChooseLevel, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                levelChoosePageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                 
                PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
            }
        }

        public void ChooseLevel(int id)
        {
            if (LevelChooseControler.LevelIsOpened(id))
            {
                LevelChooseControler.SetCurrentLevel(id);
                LevelChooseAudioControlerObj.SetCurrentLevelMusic(id);
            }
        }

        public void BuyLevel(int id)
        {
            if (CoinsControler.GetCoinsCount() >= LevelChooseControler.GetLevelById(id).Cost && !LevelChooseControler.LevelIsOpened(id))
            {
                CoinsControler.BuySegment(LevelChooseControler.GetLevelById(id).Cost);
                
                PersonStorageContoler.AddSegmentToPerson(id);
                LevelChooseControler.OpenLevel(id);
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            }
        }
        
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}