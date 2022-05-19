using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageIndicatorPanelView : MonoBehaviour
{
    [SerializeField] private Image pageIndicatorItemPb;
    [SerializeField] private List<Image> pageIndicatorList = new List<Image>();
    
    public void InitView(int pageCount)
    {
        Debug.Log(pageCount);
        for (int i = 0; i < pageCount; i++)
        {
            pageIndicatorList.Add(Instantiate(pageIndicatorItemPb,transform));
        }
    }

    public void UpdateView(int currentPage)
    {
        for (int i = 0; i < pageIndicatorList.Count; i++)
        {
            pageIndicatorList[i].color = new Color32(36,38,46,255);
        }
        pageIndicatorList[currentPage].color = new Color32(255,255,255,255);
    }
}
