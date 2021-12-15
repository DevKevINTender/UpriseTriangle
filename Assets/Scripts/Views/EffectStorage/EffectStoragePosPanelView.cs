using System.Collections.Generic;
using Controlers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Views.EffectStorage
{
    public class EffectStoragePosPanelView : MonoBehaviour
    {
        [SerializeField] private Image PosItemPb;
        [SerializeField] private Sprite ActivePosItemImage;
        [SerializeField] private Sprite UnactivePosItemImage;
        [SerializeField] private Transform ItemPanelPos;

        [SerializeField] private Text currentItemPos;
        [SerializeField] private Text maxItemPos;
        
        [SerializeField] private List<Image> PostItemList = new List<Image>();
        
        private EffectListScrObj EffectListSO;
        
        public void InitView(EffectListScrObj EffectListSO)
        {
            this.EffectListSO = EffectListSO;
            currentItemPos.text = $"{EffectListSO.CurrentEffectId + 1}";
            maxItemPos.text = $"{EffectListSO.List.Count}";
            GeneratePosItem();
        }

        public void UpdateView(int CurrentShowId)
        {
            currentItemPos.text = $"{CurrentShowId + 1}";

            foreach (var item in EffectListSO.List)
            {
                Image newItemPos = PostItemList[item.Id];
                if (item.Id == CurrentShowId)
                {
                    newItemPos.sprite = ActivePosItemImage;
                }
                else
                {
                    newItemPos.sprite = UnactivePosItemImage;
                }

                if (EffectStorageContoler.ItemIsOpened(item.Id))
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
            foreach (var item in EffectListSO.List)
            {
                Image newItemPos = Instantiate(PosItemPb, ItemPanelPos);
                if (item.Id == EffectListSO.CurrentEffectId)
                {
                    newItemPos.sprite = ActivePosItemImage;
                }
                else
                {
                    newItemPos.sprite = UnactivePosItemImage;
                }

                if (EffectStorageContoler.ItemIsOpened(item.Id))
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