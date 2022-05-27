using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LevelChoosePageView : MonoBehaviour
    {
        [SerializeField] private LevelChooseItemView levelChooseItemViewPb;
        [SerializeField] private List<LevelChooseItemView> List = new List<LevelChooseItemView>();
        private List<SessionLevelScrObj> sessionLevelList = new List<SessionLevelScrObj>();

        private LSItemAction buyLevel;
        private LSItemAction chooseLevel;
        private LSPageAction showNext;
        private LSPageAction showPrevious;

        public void InitView(List<SessionLevelScrObj> personList, LSItemAction buyLevel, LSItemAction chooseLevel,
            LSPageAction showNext, LSPageAction showPrevious)
        {
            this.buyLevel = buyLevel;
            this.chooseLevel = chooseLevel;

            this.sessionLevelList = personList;
            foreach (var item in personList)
            { 
                LevelChooseItemView newItem = Instantiate(levelChooseItemViewPb, transform);
                newItem.InitView(item, this.buyLevel, this.chooseLevel, UpdateView, showNext, showPrevious);
                List.Add(newItem);
            }
        }

        public void UpdateView()
        {
            for (int i = 0; i < sessionLevelList.Count; i++)
            {
                List[i].UpdateView();
            }
        }
    }
}