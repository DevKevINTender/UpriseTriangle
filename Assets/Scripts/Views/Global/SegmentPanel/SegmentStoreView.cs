using System.Collections;
using System.Collections.Generic;
using Controlers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Views.Global;

public class SegmentStoreView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Text coinCountText;
    [SerializeField] private Text segmentCountText;
    [SerializeField] private Text boughtSegmentCountText;

    [SerializeField] private Transform SegmentPanelPos;
    [SerializeField] private GameObject boughtSegmentPanel;

    [SerializeField] private PageIndicatorPanelView PageIndicatorPanelView;
    [SerializeField] private List<RectTransform> pageList = new List<RectTransform>();
    [SerializeField] private RectTransform currentPageObj;
    [SerializeField] private AlertPanelView AlertPanelViewPb;
    [SerializeField] private int pageCount;
    private int currentPage;
    
    public Vector3 startDragVector;
    public Vector3 endDragVector;

    public delegate void segmentBuyDelegate(int count);
    
    void Start()
    {
        currentPageObj = Instantiate(pageList[currentPage], SegmentPanelPos);
        currentPageObj.GetComponent<SegmentBuyPanelItemView>().InitView(BuySegment);
        coinCountText.text = "" +  CoinsControler.GetCoinsCount();
        segmentCountText.text = "" +  SegmentControler.GetSegmentCount();
        PageIndicatorPanelView.InitView(pageCount);
        PageIndicatorPanelView.UpdateView(currentPage);
    }

    public void BuySegment(int count)
    {
        if (SegmentControler.BuySegment(count))
        {
            coinCountText.text = "" +  CoinsControler.GetCoinsCount();
            segmentCountText.text = "" +  SegmentControler.GetSegmentCount();
            boughtSegmentPanel.SetActive(true);
            boughtSegmentCountText.text = "x" + count;
        }
        else
        {
            AlertPanelView alert = Instantiate(AlertPanelViewPb, transform);
            alert.InitView("Проблемка", "Где деньги Лебовски ?");
        }
    }
    public void ShowPreviousPage()
    {
        if (currentPage > 0)
        {
            RectTransform previousPage = currentPageObj;
            previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
            
            currentPage--;
            currentPageObj = Instantiate(pageList[currentPage], SegmentPanelPos);
            currentPageObj.GetComponent<SegmentBuyPanelItemView>().InitView(BuySegment);
            currentPageObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1500,0);
            PageIndicatorPanelView.UpdateView(currentPage);
            
            currentPageObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
        }
    }

    public void ShowNextPage()
    {
        if (currentPage < pageCount - 1)
        {
            RectTransform previousPage = currentPageObj;
            previousPage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(previousPage.gameObject);});
            
            currentPage++;
            currentPageObj = Instantiate(pageList[currentPage], SegmentPanelPos);
            currentPageObj.GetComponent<SegmentBuyPanelItemView>().InitView(BuySegment);
            currentPageObj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1500,0);
            PageIndicatorPanelView.UpdateView(currentPage);

            currentPageObj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
        }
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        endDragVector = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragVector = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (endDragVector.x > startDragVector.x)
        {
            ShowPreviousPage();
        }
        else
        {
            ShowNextPage();
        }
    }
}
