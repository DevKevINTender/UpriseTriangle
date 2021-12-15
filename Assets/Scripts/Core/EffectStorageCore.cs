using Controlers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Views.EffectStorage;

namespace Core
{
    public class EffectStorageCore : MonoBehaviour
    {
        [SerializeField] private EffectListScrObj EffectListSO;
        

        [SerializeField] private EffectStoragePanelView EffectStoragePanelViewObj;
        [SerializeField] private EffectStoragePosPanelView EffectStoragePosPanelViewObj;

        public int CurrentEffectShowId;
        public void Start()
        {
            EffectListSO.Load();
            EffectStoragePanelViewObj.InitView(this);
            EffectStoragePosPanelViewObj.InitView(EffectListSO);
        }

        public void ShowNextEffect()
        {
            if (CurrentEffectShowId < EffectListSO.List.Count - 1)
            {
                CurrentEffectShowId++;
                EffectStoragePosPanelViewObj.UpdateView(CurrentEffectShowId);
            }
        }

        public void ShowPreviousEffect()
        {
            if (CurrentEffectShowId > 0)
            {
                CurrentEffectShowId--;
                EffectStoragePosPanelViewObj.UpdateView(CurrentEffectShowId);
            }
        }

        public void StartSession()
        {
            
        }
    }
}