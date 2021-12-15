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
        
        public GameObject BuyBtn;
        public GameObject PlayBtn;
        public GameObject BackBtn;

        public void InitView(EffectStorageCore EffectStorageCoreObj)
        {
            this.EffectStorageCoreObj = EffectStorageCoreObj;
        }

        public void UpdateView()    
        {
            
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