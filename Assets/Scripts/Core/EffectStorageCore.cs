using Components;
using Controlers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Views.EffectStorage;
using System.Collections;

namespace Core
{
    public class EffectStorageCore : MonoBehaviour
    {
        [SerializeField] public EffectListScrObj EffectListSO;
        [SerializeField] public Text StorageCoins;

        [SerializeField] private EffectStoragePanelView EffectStoragePanelViewObj;
        [SerializeField] private EffectStoragePosPanelView EffectStoragePosPanelViewObj;
        [SerializeField] private EffectStorageListComponent EffectStorageListComponent;

        [SerializeField] private InfoPanelView InfoPanelPb;
        [SerializeField] private Transform infoPanelPos;

        [SerializeField] private Transform EffectStorageComponent;

        public int CurrentEffectShowId;
        public void Start()
        {
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            EffectListSO.Load();
            CurrentEffectShowId = EffectListSO.CurrentEffectId;
            EffectStoragePanelViewObj.InitView(this);
            EffectStoragePosPanelViewObj.InitView(EffectListSO);
            EffectStorageListComponent.InitComponent(EffectListSO);
        }

        public void ShowNextEffect()
        {
            if (CurrentEffectShowId < EffectListSO.List.Count - 1)
            {
                CurrentEffectShowId++;
                EffectStoragePosPanelViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                EffectStoragePanelViewObj.UpdateView();
                EffectStorageContoler.SetCurrentEffect(CurrentEffectShowId);
            }
        }

        public void ShowPreviousEffect()
        {
            if (CurrentEffectShowId > 0)
            {
                CurrentEffectShowId--;
                EffectStoragePosPanelViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                EffectStoragePanelViewObj.UpdateView();
                EffectStorageContoler.SetCurrentEffect(CurrentEffectShowId);
            }
        }

        public void BuyEffect()
        {
            if (CoinsControler.BuyEffect(EffectListSO.List[CurrentEffectShowId].Cost))
            {
                EffectStorageContoler.OpenPerson(CurrentEffectShowId);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
              
                EffectStoragePosPanelViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                EffectStoragePanelViewObj.UpdateView();
            }
            else
            {
                InfoPanelView newInfoPanel = Instantiate(InfoPanelPb, infoPanelPos);
                newInfoPanel.InitView("Cash", "You havent money");
            }
            
        }
        public void StartSession()
        {
            if (EffectStorageContoler.ItemIsOpened(CurrentEffectShowId))
            {
                LaunchPlayer();
            }
            else
            {
                InfoPanelView newInfoPanel = Instantiate(InfoPanelPb, infoPanelPos);
                newInfoPanel.InitView("Exist", "What you try to do ? Am ?");
            }
        }
        
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }


        public void LaunchPlayer()
        {
            foreach (Transform child in EffectStorageComponent)
            {
                child.GetComponent<Animation>().CrossFade("Togame");
               // StartCoroutine(WaitAnimationEnd(child.GetComponent<Animation>().clip.length));
            }
        }


        private IEnumerator WaitAnimationEnd(float _time)
        {
            yield return new WaitForSeconds(_time);
            SceneManager.LoadScene(1);
        }

    }
}