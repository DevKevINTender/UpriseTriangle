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
using DOTweenAnimation.Global;
using Views.PersonStorage;
using Random = UnityEngine.Random;

namespace Core
{
    public  delegate  void PersonItemAction(int id);
    public  delegate  void PersonPageAction();
    
    public class PersonStorageCore : MonoBehaviour
    {
        [SerializeField] public Transform canvas;

        [SerializeField] public PersonListScrObj PersonListSO;
        [SerializeField] public Text StorageCoins;
        [SerializeField] public Text StorageSegments;
        [SerializeField] public PersonPageView PersonPageViewPb;
        [SerializeField] public PersonPageView PersonPageViewCurrentObj;
        
        [SerializeField] public PSPanelView PSPanelViewObj;
        [SerializeField] private AlertPanelView alertPanelPb;
        [SerializeField] private Transform infoPanelPos;
        
        [SerializeField] private SegmentStoreView SegmentStoreView;
        [SerializeField] private PageIndicatorPanelView PageIndicatorPanelViewObj;
        [SerializeField] private TransitionAnimation TransitionAnimation;
        public int CurrentPersonShowId;
        public double CurrentPageId;
        public double PageCount;
        public int PageState; // 0 - choose 1 - buy
        
        public void Start()
        {
            TransitionAnimation.gameObject.SetActive(true);
            TransitionAnimation.OpenScene();
            
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
            PersonListSO.Load();
            CurrentPersonShowId = PersonListSO.CurrentPersonId;
            CurrentPageId =  Math.Floor((double) CurrentPersonShowId / 4);
            PageCount = Math.Ceiling((double) PersonListSO.List.Count / 4);
            PersonPageViewCurrentObj = Instantiate(PersonPageViewPb,canvas);
            PersonPageViewCurrentObj.InitView(PersonStorageContoler.GetPersonItemForPage((int)CurrentPageId), BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
            PSPanelViewObj.InitView(this);
            PageIndicatorPanelViewObj.InitView((int)PageCount);
            PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
        }
        
        public void ShowNextPage()
        {
            if (CurrentPageId < PageCount-1)
            {
                CurrentPageId++;
                PersonPageView previousPage = PersonPageViewCurrentObj;
                PersonPageViewCurrentObj = Instantiate(PersonPageViewPb,canvas);
                PersonPageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1500,0);
                PersonPageViewCurrentObj.InitView(PersonStorageContoler.GetPersonItemForPage((int)CurrentPageId),BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                PersonPageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                
                PageIndicatorPanelViewObj.UpdateView((int)CurrentPageId);
            }
            CheckPageState();
        }

        public void ShowPreviousPage()
        {
            if (CurrentPageId > 0)
            {
                CurrentPageId--;
                PersonPageView previousPage = PersonPageViewCurrentObj;
                PersonPageViewCurrentObj = Instantiate(PersonPageViewPb,canvas);
                PersonPageViewCurrentObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1500,0);
                PersonPageViewCurrentObj.InitView(PersonStorageContoler.GetPersonItemForPage((int)CurrentPageId), BuySegment, ChoosePerson, ShowNextPage, ShowPreviousPage);
                previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
                PersonPageViewCurrentObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
                 
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
                PersonPageViewCurrentObj.UpdateViewItem(list[ChoosendId].Id);
                SegmentControler.DecreaseSegment(1);
                StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
            }
            else
            {
                SegmentStoreView.gameObject.SetActive(true);
            }
        }

        public void ShowBuySegmentPanel()
        {
            PersonPageViewCurrentObj.ShowBuySegmentPanel();
            PageState = 1;
        }
        
        public void HideBuySegmentPanel()
        {
            PersonPageViewCurrentObj.HideBuySegmentPanel();
            PageState = 0;
        }
        
        
        public void ChoosePerson(int id)
        {
            if (PersonStorageContoler.ItemIsOpened(id))
            {
                PersonStorageContoler.SetCurrentPerson(id);
            }
        }

        public void BuySegment(int id)
        {
            if (CoinsControler.GetCoinsCount() >= 500 && !PersonStorageContoler.ItemIsOpened(id))
            {
                CoinsControler.BuySegment(500);
                PersonStorageContoler.AddSegmentToPerson(id);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
                StorageSegments.text = $"X{SegmentControler.GetSegmentCount()}";
                
                ShowBuySegmentPanel();
            }
        }
        
        public void BackToMenu()
        {
            TransitionAnimation.CloseScene(0, "MainMenu");
        }
    }
}