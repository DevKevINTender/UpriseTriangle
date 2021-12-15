using System.Collections.Generic;
using Controlers;
using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LevelPosPanelView : MonoBehaviour
    {
        [SerializeField] private Image PosItemPb;
        [SerializeField] private Sprite ActivePosItemImage;
        [SerializeField] private Sprite UnactivePosItemImage;
        [SerializeField] private Transform ItemPanelPos;

        [SerializeField] private Text currentItemPos;
        [SerializeField] private Text maxItemPos;
        
        [SerializeField] private List<Image> PostItemList = new List<Image>();
        
        private SessionLevelListScrObj SessionLevelListSO;
        
        public void InitView(SessionLevelListScrObj SessionLevelListSO)
        {
            this.SessionLevelListSO = SessionLevelListSO;
            currentItemPos.text = $"{SessionLevelListSO.CurrentSessionLevelId + 1}";
            maxItemPos.text = $"{SessionLevelListSO.List.Count}";
            GeneratePosItem();
        }

        public void UpdateView(int CurrentShowId)
        {
            currentItemPos.text = $"{CurrentShowId + 1}";

            foreach (var item in SessionLevelListSO.List)
            {
                Image newItemPos = PostItemList[item.LevelId];
                if (item.LevelId == CurrentShowId)
                {
                    newItemPos.sprite = ActivePosItemImage;
                }
                else
                {
                    newItemPos.sprite = UnactivePosItemImage;
                }

                if (SessionLevelControler.LevelIsOpened(item.LevelId))
                {
                    newItemPos.color = new Color32(255,255,255, 255);
                }
                else
                {
                    newItemPos.color = new Color32(36,38,46, 255);
                }
            }
        }
        public void GeneratePosItem()
        {
            foreach (var item in SessionLevelListSO.List)
            {
                Image newItemPos = Instantiate(PosItemPb, ItemPanelPos);
                if (item.LevelId == SessionLevelListSO.CurrentSessionLevelId)
                {
                    newItemPos.sprite = ActivePosItemImage;
                }
                else
                {
                    newItemPos.sprite = UnactivePosItemImage;
                }

                if (SessionLevelControler.LevelIsOpened(item.LevelId))
                {
                    newItemPos.color = new Color32(255,255,255, 255);
                }
                else
                {
                    newItemPos.color = new Color32(36,38,46, 255);
                }
                PostItemList.Add(newItemPos);
            } 
        }
    }
}