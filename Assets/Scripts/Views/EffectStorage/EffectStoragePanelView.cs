using Controlers;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.EffectStorage
{
    public class EffectStoragePanelView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private EffectStorageCore EffectStorageCoreObj;
        
        public Text EffectName;
        public Text EffectSlogan;

        public Text EffectCost;
        
        public GameObject BuyBtn;
        public GameObject PlayBtn;
        public GameObject BackBtn;

        public void InitView(EffectStorageCore EffectStorageCoreObj)
        {
            this.EffectStorageCoreObj = EffectStorageCoreObj;
            UpdateView();
        }

        public void UpdateView()
        {
            EffectName.text = $"{EffectStorageCoreObj.EffectListSO.List[EffectStorageCoreObj.CurrentEffectShowId].EffectName}";
            EffectSlogan.text = $"{EffectStorageCoreObj.EffectListSO.List[EffectStorageCoreObj.CurrentEffectShowId].EffectSlogan}";

            if (EffectStorageContoler.ItemIsOpened(EffectStorageCoreObj.CurrentEffectShowId))
            {
                BuyBtn.SetActive(false);
                //PlayBtn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                PlayBtn.GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                EffectCost.text = $"{EffectStorageCoreObj.EffectListSO.List[EffectStorageCoreObj.CurrentEffectShowId].Cost}";
            }
            else
            {
                BuyBtn.SetActive(true);
                //PlayBtn.GetComponent<Image>().color = new Color32(36, 38, 46, 255);
                PlayBtn.GetComponentInChildren<Text>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
            }
        }

        public void BuyEffect()
        {
            EffectStorageCoreObj.BuyEffect();
        }

        public void StartSession()
        {
            EffectStorageCoreObj.StartSession();
        }

        public void BackToMenu()
        {
            EffectStorageCoreObj.BackToMenu();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
     
                if (eventData.delta.x > 0)
                {
                    EffectStorageCoreObj.ShowPreviousEffect();
                }
                else
                {
                    EffectStorageCoreObj.ShowNextEffect();
                }
            
        }
    }
}