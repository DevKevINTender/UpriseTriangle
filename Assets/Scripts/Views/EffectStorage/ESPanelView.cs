using Controlers;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.EffectStorage
{
    public class ESPanelView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private EffectStorageCore EffectStorageCoreObj;
        
        public Text EffectName;
        public Text EffectSlogan;

        public Text EffectCost;
        
        public GameObject BuyBtn;
        public GameObject PlayBtn;
        public GameObject BackBtn;

        public Vector3 startDragVector;
        public Vector3 endDragVector;
        
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
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                BuyBtn.SetActive(true);
                EffectCost.text = $"{EffectStorageCoreObj.EffectListSO.List[EffectStorageCoreObj.CurrentEffectShowId].Cost}";
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(36, 38, 46, 255);
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
            startDragVector = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            endDragVector = eventData.pointerCurrentRaycast.worldPosition;
                if (endDragVector.x > startDragVector.x)
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