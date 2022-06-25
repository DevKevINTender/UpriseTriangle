using Controlers;
using Core;
using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LevelChooseItemView : MonoBehaviour
    {
        [SerializeField] private GameObject choosenBtn;
        [SerializeField] private GameObject availableBtn;
        [SerializeField] private GameObject buyBtn;

        [SerializeField] private GameObject selectGradient;
        [SerializeField] private Image statusBorder;

        [SerializeField] private Text attempCount;
        [SerializeField] private Text coinCollectCount;
        [SerializeField] private Text completePercent;
        
        [SerializeField] private Text LevelCost;
        [SerializeField] private Text MusicInfo;
        [SerializeField] private Text Id;
        [SerializeField] private Text Complexity;
        
        

        private LSItemAction buyLevel;
        private LSItemAction chooseLevel;
        private LSPageAction showNext;        
        private LSPageAction updateView;
        private LSPageAction showPrevious;
        
        
        public Vector2 startTouch;
        public Vector2 endTouch;

        [SerializeField] private int id;
        public void InitView(SessionLevelScrObj personScrObj, LSItemAction buyLevel,  LSItemAction chooseLevel, LSPageAction updateView, LSPageAction showNext, LSPageAction showPrevious)
        {
            this.buyLevel = buyLevel;
            this.chooseLevel = chooseLevel;
            this.updateView = updateView;
            this.showNext = showNext;
            this.showPrevious = showPrevious;
            
            id = personScrObj.Id;
            UpdateView();
        }

        public void UpdateView()
        {
            SessionLevelScrObj levelScrObj = LevelChooseControler.GetLevelById(id);
            
            choosenBtn.SetActive(false);
            availableBtn.SetActive(false);
            buyBtn.SetActive(false);
            
            MusicInfo.text = $"{levelScrObj.MusicName} / {levelScrObj.MusicCreator}";
            LevelCost.text = $"{levelScrObj.Cost}";
            Complexity.text = $"ур. сложности - {levelScrObj.levelComplexity}";
            
            Id.text = $"{levelScrObj.Id}";
            
            if (LevelChooseControler.LevelIsOpened(id))
            {
                if (LevelChooseControler.GetCurrentLevel() == id)
                {
                    selectGradient.SetActive(true);
                    choosenBtn.SetActive(true);
                }
                else
                {
                    selectGradient.SetActive(false);
                    availableBtn.SetActive(true);
                }
                statusBorder.color = new Color32(46,255,193,255);
                attempCount.text = $"{levelScrObj.AttempCount}";
                coinCollectCount.text = $"{levelScrObj.CoinsCollectCount}";
                completePercent.text = $"{levelScrObj.CompletePercent}%";

            }
            else
            {
                buyBtn.SetActive(true);
            }
            
            
        }

        public void BuyLevel()
        {
            buyLevel?.Invoke(id);
            updateView?.Invoke();
        }

        public void ChooseCurrentLevel()
        {
            chooseLevel?.Invoke(id);
            updateView?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Item: " + id + " Pressed");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Item: " + id + " UnPressed");
        }

        public void OnDrag(PointerEventData eventData)
        {
            endTouch = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startTouch = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (endTouch.x > startTouch.x)
            {
                showPrevious?.Invoke();
            }
            else
            {
                showNext?.Invoke();
            }
        }
    }
}