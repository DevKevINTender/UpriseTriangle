using System.Collections;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;



public class SkillPageView : MonoBehaviour
{
    [SerializeField] private SkillItemView skillItemViewPb;
    [SerializeField] private List<SkillItemView> List = new List<SkillItemView>();
    private List<SkillScrObj> skillList = new List<SkillScrObj>();
    
    private SkillItemAction buySegment;
    private SkillItemAction chooseSegment;
    private SkillPageAction showNext;
    private SkillPageAction showPrevious;

    public void InitView(List<SkillScrObj> skillList, SkillItemAction buySegment, SkillItemAction chooseSegment, SkillPageAction showNext, SkillPageAction showPrevious)
    {
        this.buySegment = buySegment;
        this.chooseSegment = chooseSegment;

        this.skillList = skillList;
        foreach (var item in skillList)
        {
            SkillItemView newItem = Instantiate(skillItemViewPb, transform);
            newItem.InitView(item,this.buySegment, this.chooseSegment, UpdateView, showNext, showPrevious);
            List.Add(newItem);
        }
    }
    public void UpdateView()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            List[i].UpdateView();
        }
    }
    public void UpdateViewItem(int id)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].Id == id)
            {
                List[i].UpdateView(id);
            }
        }
    }

    public void ShowBuySegmentPanel()
    {
        foreach (var item in List)
        {
            item.ShowBuySegmentPanel();
        }
    }

    public void HideBuySegmentPanel()
    {
        foreach (var item in List)
        {
            item.HideBuySegmentPanel();
        }
    }
}
