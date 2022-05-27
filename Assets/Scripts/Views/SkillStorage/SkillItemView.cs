using System.Collections;
using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.EventSystems;
public class SkillItemView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    private SkillItemAction buySegment;
    private SkillItemAction chooseSegment;
    private SkillPageAction updateView;
    private SkillPageAction showNext;
    private SkillPageAction showPrevious;
    
    
    public Vector2 startTouch;
    public Vector2 endTouch;

    [SerializeField] private int id;
    public void InitView(SkillScrObj skillScrObj, SkillItemAction buySegment,  SkillItemAction chooseSegment, SkillPageAction updateView, SkillPageAction showNext, SkillPageAction showPrevious)
    {
        this.buySegment = buySegment;
        this.chooseSegment = chooseSegment;
        this.updateView = updateView;
        this.showNext = showNext;
        this.showPrevious = showPrevious;
        
        id = skillScrObj.Id;
        idText.text = ""+id;
        UpdateView(id);
    }

    public void UpdateView()
    {
        segment.SetActive(false);
        available.SetActive(false);
        choosen.SetActive(false);
        
        if (SkillStorageContoler.ItemIsOpened(id))
        {
            available.SetActive(true);
            segment.SetActive(false);
            borderStatus.color = new Color32(46,255,193,255);
        }
        else
        {
            SkillScrObj skillScrObj = SkillStorageContoler.GetSkillById(id);
            segment.SetActive(true);
            segmentCount.text = $"{skillScrObj.CurrentSegment}/{skillScrObj.RequiredSegments}";
            segmentCountBuy.text = $"{skillScrObj.CurrentSegment}/{skillScrObj.RequiredSegments}";
        }
        if (SkillStorageContoler.GetCurrentSkill() == id)
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
        
        if (SkillStorageContoler.ItemIsOpened(id))
        {
            available.SetActive(true);
            segment.SetActive(false);
            borderStatus.color = new Color32(46,255,193,255);
        }
        else
        {
            SkillScrObj skillScrObj = SkillStorageContoler.GetSkillById(id);
            segment.SetActive(true);
            segmentCount.text = $"{skillScrObj.CurrentSegment}/{skillScrObj.RequiredSegments}";
            segmentCountBuy.text = $"{skillScrObj.CurrentSegment}/{skillScrObj.RequiredSegments}";
        }
        if (SkillStorageContoler.GetCurrentSkill() == id)
        {
            available.SetActive(false);
            choosen.SetActive(true);
        }
    }
    public void ShowBuySegmentPanel()
    {
        SkillScrObj skillScrObj = SkillStorageContoler.GetSkillById(id);
        if (skillScrObj.CurrentSegment >= skillScrObj.RequiredSegments)
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

    public void ChooseCurrentSkill()
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
