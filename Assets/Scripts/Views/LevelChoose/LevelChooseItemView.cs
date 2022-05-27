using Controlers;
using Core;
using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LSPanelItemView : MonoBehaviour
    {
        [SerializeField] private GameObject choosen;
        [SerializeField] private GameObject available;
        [SerializeField] private GameObject choosenBorder;
        [SerializeField] private GameObject segment;
        [SerializeField] private Text segmentCount;
        [SerializeField] private Image topBorderStatus;

        [SerializeField] private GameObject CompleteSegmentPanel;
        [SerializeField] private GameObject AddBuySegmentPanel;
        [SerializeField] private Text segmentCountBuy;
        
        [SerializeField] 

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
            UpdateView(id);
        }

        public void UpdateView()
        {
            segment.SetActive(false);
            available.SetActive(false);
            choosen.SetActive(false);
            
            if (PersonStorageContoler.ItemIsOpened(id))
            {
                available.SetActive(true);
                segment.SetActive(false);
                topBorderStatus.color = new Color32(46,255,193,255);
            }
            else
            {
                PersonScrObj personScrObj = PersonStorageContoler.GetPersonById(id);
                segment.SetActive(true);
                segmentCount.text = $"{personScrObj.CurrentSegment}/{personScrObj.RequiredSegments}";
                segmentCountBuy.text = $"{personScrObj.CurrentSegment}/{personScrObj.RequiredSegments}";
            }
            if (PersonStorageContoler.GetCurrentPerson() == id)
            {
                available.SetActive(false);
                choosen.SetActive(true);
            }
        }
        public void UpdateView(int id)
        {
            segment.SetActive(false);
            available.SetActive(false);
            choosen.SetActive(false);
            
            if (PersonStorageContoler.ItemIsOpened(id))
            {
                available.SetActive(true);
                segment.SetActive(false);
                topBorderStatus.color = new Color32(46,255,193,255);
            }
            else
            {
                PersonScrObj personScrObj = PersonStorageContoler.GetPersonById(id);
                segment.SetActive(true);
                segmentCount.text = $"{personScrObj.CurrentSegment}/{personScrObj.RequiredSegments}";
                segmentCountBuy.text = $"{personScrObj.CurrentSegment}/{personScrObj.RequiredSegments}";
            }
            if (PersonStorageContoler.GetCurrentPerson() == id)
            {
                available.SetActive(false);
                choosen.SetActive(true);
            }
        }
        public void ShowBuySegmentPanel()
        {
            PersonScrObj personScrObj = PersonStorageContoler.GetPersonById(id);
            if (personScrObj.CurrentSegment >= personScrObj.RequiredSegments)
            {
                CompleteSegmentPanel.SetActive(true);
                AddBuySegmentPanel.SetActive(false);
            }
            else
            {
                CompleteSegmentPanel.SetActive(false);
                AddBuySegmentPanel.SetActive(true);
            }
            
        }
        public void HideBuySegmentPanel()
        {
            CompleteSegmentPanel.SetActive(false);
            AddBuySegmentPanel.SetActive(false);
        }
        public void BuySegment()
        {
            buyLevel?.Invoke(id);
            updateView?.Invoke();
        }

        public void ChooseCurrentPerson()
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