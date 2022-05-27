using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LSPageView : MonoBehaviour
    {
        [SerializeField] private LSPanelItemView LSPanelItemViewPB;
        [SerializeField] private List<LSPanelItemView> List = new List<LSPanelItemView>();
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
                LSPanelItemView newItem = Instantiate(LSPanelItemViewPB, transform);
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

        public void UpdateViewItem(int id)
        {
            for (int i = 0; i < sessionLevelList.Count; i++)
            {
                if (sessionLevelList[i].Id == id)
                {
                    List[i].UpdateView(id);
                }
            }
        }
    }
}