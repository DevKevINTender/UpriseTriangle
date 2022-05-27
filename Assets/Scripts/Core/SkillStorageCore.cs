using System;
using Components;
using Controlers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.EffectStorage;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Views.PersonStorage;
using Random = UnityEngine.Random;

namespace Core
{
    public  delegate  void SkillItemAction(int id);
    public  delegate  void SkillPageAction();
    
    public class SkillStorageCore : MonoBehaviour
    {
        [SerializeField] public Transform canvas;

        [SerializeField] public SkillListScrObj SkillListSO;
        [SerializeField] public SkillPageView SkillPageViewPb;
        [SerializeField] public SkillPageView SkillPageViewCurrentObj;
        
        [SerializeField] public Text StorageCoins;
        [SerializeField] public Text StorageSegments;
        
        [SerializeField] public SkillPanelView SkillPanelViewObj;
        [SerializeField] private AlertPanelView alertPanelPb;
        [SerializeField] private Transform infoPanelPos;

        [SerializeField] private PageIndicatorPanelView PageIndicatorPanelViewObj;

        public int CurrentSkillShowId;
        public double CurrentPageId;
        public double PageCount;
        public int PageState; // 0 - choose 1 - buy
        
        public void Start()
        {
            SegmentControler.UpcreaseSegment(10);
            CoinsControler.UpcreaseCoins(10000);
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
            SkillListSO.Load();
            CurrentSkillShowId = SkillListSO.CurrentSkillId;
            CurrentPageId =  Math.Floor((double) CurrentSkillShowId / 9);
            PageCount = Math.Ceiling((double) SkillListSO.List.Count / 9);
            SkillPageViewCurrentObj = Instantiate(SkillPageViewPb,canvas);
            SkillPageViewCurrentObj.InitView(SkillStorageContoler.GetSkillItemForPage((int)CurrentPageId), BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
            SkillPanelViewObj.InitView(this);
            PageIndicatorPanelViewObj.InitView((int)PageCount);
            PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
        }
        
        public void ShowNextPage()
        {
            if (CurrentPageId < PageCount-1)
            {
                CurrentPageId++;
                SkillPageView previousPage = SkillPageViewCurrentObj;
                SkillPageViewCurrentObj = Instantiate(SkillPageViewPb,canvas);
                SkillPageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1500,0);
                SkillPageViewCurrentObj.InitView(SkillStorageContoler.GetSkillItemForPage((int)CurrentPageId),BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                SkillPageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                
                PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
            }
            CheckPageState();
        }

        public void ShowPreviousPage()
        {
            if (CurrentPageId > 0)
            {
                CurrentPageId--;
                SkillPageView previousPage = SkillPageViewCurrentObj;
                SkillPageViewCurrentObj = Instantiate(SkillPageViewPb,canvas);
                SkillPageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1500,0);
                SkillPageViewCurrentObj.InitView(SkillStorageContoler.GetSkillItemForPage((int)CurrentPageId), BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                SkillPageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                 
                PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
            }
            CheckPageState();
        }

        private void CheckPageState()
        {
            switch (PageState)
            {
                case 0:
                {
                    HideBuySegmentPanel();
                    break;
                }
                case 1:
                {
                    ShowBuySegmentPanel();
                    break;
                }
            }
        }
        public void OpenSegment()
        {
            if (SegmentControler.GetSegmentCount() > 0)
            {
                List<PersonScrObj> list = PersonStorageContoler.GetNotOpenedPersons();
                int ChoosendId = Random.Range(0,list.Count);
                PersonStorageContoler.AddSegmentToPerson(list[ChoosendId].Id);
                SkillPageViewCurrentObj.UpdateViewItem(list[ChoosendId].Id);
                SegmentControler.DecreaseSegment(1);
                StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
            }
        }

        public void ShowBuySegmentPanel()
        {
            SkillPageViewCurrentObj.ShowBuySegmentPanel();
            PageState = 1;
        }
        
        public void HideBuySegmentPanel()
        {
            SkillPageViewCurrentObj.HideBuySegmentPanel();
            PageState = 0;
        }
        
        
        public void ChoosePerson(int id)
        {
            if (SkillStorageContoler.ItemIsOpened(id))
            {
                SkillStorageContoler.SetCurrentSkill(id);
            }
        }

        public void BuySegment(int id)
        {
            if (CoinsControler.GetCoinsCount() >= 500 && !SkillStorageContoler.ItemIsOpened(id))
            {
                CoinsControler.BuySegment(500);
                SkillStorageContoler.AddSegmentToSkill(id);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
                StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
                
                ShowBuySegmentPanel();
            }
        }
        
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}