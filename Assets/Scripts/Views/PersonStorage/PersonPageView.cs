using System.Collections;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;



public class PersonPageView : MonoBehaviour
{
    [SerializeField] private PersonItemView personItemViewPb;
    [SerializeField] private List<PersonItemView> List = new List<PersonItemView>();
    private List<PersonScrObj> personlist = new List<PersonScrObj>();
    
    private PersonItemAction buySegment;
    private PersonItemAction chooseSegment;
    private PersonPageAction showNext;
    private PersonPageAction showPrevious;

    public void InitView(List<PersonScrObj> personList, PersonItemAction buySegment, PersonItemAction chooseSegment, PersonPageAction showNext, PersonPageAction showPrevious)
    {
        this.buySegment = buySegment;
        this.chooseSegment = chooseSegment;

        this.personlist = personList;
        foreach (var item in personList)
        {
            PersonItemView newItem = Instantiate(personItemViewPb, transform);
            newItem.InitView(item,this.buySegment, this.chooseSegment, UpdateView, showNext, showPrevious);
            List.Add(newItem);
        }
    }
    public void UpdateView()
    {
        for (int i = 0; i < personlist.Count; i++)
        {
            List[i].UpdateView();
        }
    }
    public void UpdateViewItem(int id)
    {
        for (int i = 0; i < personlist.Count; i++)
        {
            if (personlist[i].Id == id)
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
