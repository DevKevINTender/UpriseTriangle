using System.Collections;
using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.EventSystems;
public class PersonItemView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject choosen;
    [SerializeField] private GameObject available;
    [SerializeField] private GameObject choosenBorder;
    [SerializeField] private GameObject segment;
    [SerializeField] private Text segmentCount;
    [SerializeField] private Image borderStatus;
    [SerializeField] private Text idText;

    [SerializeField] private GameObject CompleteSegmentPanel;
    [SerializeField] private GameObject AddBuySegmentPanel;
    [SerializeField] private Text segmentCountBuy;
    
    [SerializeField] 

    private PersonItemAction buySegment;
    private PersonItemAction chooseSegment;
    private PersonPageAction updateView;
    private PersonPageAction showNext;
    private PersonPageAction showPrevious;
    
    
    public Vector2 startTouch;
    public Vector2 endTouch;

    [SerializeField] private int id;
    public void InitView(PersonScrObj personScrObj, PersonItemAction buySegment,  PersonItemAction chooseSegment, PersonPageAction updateView, PersonPageAction showNext, PersonPageAction showPrevious)
    {
        this.buySegment = buySegment;
        this.chooseSegment = chooseSegment;
        this.updateView = updateView;
        this.showNext = showNext;
        this.showPrevious = showPrevious;
        
        id = personScrObj.Id;
        idText.text = ""+id;
        UpdateView(id);
    }

    public void UpdateView()
    {
        segment.SetActive(false);
        available.SetActive(false);
        choosen.SetActive(false);
        choosenBorder.SetActive(false);
        
        if (PersonStorageContoler.ItemIsOpened(id))
        {
            available.SetActive(true);
            segment.SetActive(false);
            borderStatus.color = new Color32(255,255,255,255);
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
            borderStatus.color = new Color32(46,255,193,255);
            available.SetActive(false);
            choosen.SetActive(true);
            choosenBorder.SetActive(true);
        }
    }
    public void UpdateView(int id)
    {
        segment.SetActive(false);
        available.SetActive(false);
        choosen.SetActive(false);
        choosenBorder.SetActive(false);
        
        if (PersonStorageContoler.ItemIsOpened(id))
        {
            available.SetActive(true);
            segment.SetActive(false);
            borderStatus.color = new Color32(255,255,255,255);
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
            borderStatus.color = new Color32(46,255,193,255);
            available.SetActive(false);
            choosen.SetActive(true);
            choosenBorder.SetActive(true);
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
        buySegment?.Invoke(id);
        updateView?.Invoke();
    }

    public void ChooseCurrentPerson()
    {
        chooseSegment?.Invoke(id);
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
