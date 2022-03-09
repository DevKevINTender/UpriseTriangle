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

        [SerializeField] private ESPanelView esPanelViewObj;
        [SerializeField] private ESPageView esPageViewObj;
        [SerializeField] private EffectStorageListComponent EffectStorageListComponent;

        [SerializeField] private AlertPanelView alertPanelPb;
        [SerializeField] private Transform infoPanelPos;

        [SerializeField] private Transform EffectStorageComponent;

        public int CurrentEffectShowId;
        public void Start()
        {
            StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
            EffectListSO.Load();
            CurrentEffectShowId = EffectListSO.CurrentEffectId;
            esPanelViewObj.InitView(this);
            esPageViewObj.InitView(EffectListSO);
            EffectStorageListComponent.InitComponent(EffectListSO);
        }

        public void ShowNextEffect()
        {
            if (CurrentEffectShowId < EffectListSO.List.Count - 1)
            {
                CurrentEffectShowId++;
                esPageViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                esPanelViewObj.UpdateView();
                EffectStorageContoler.SetCurrentEffect(CurrentEffectShowId);
            }
        }

        public void ShowPreviousEffect()
        {
            if (CurrentEffectShowId > 0)
            {
                CurrentEffectShowId--;
                esPageViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                esPanelViewObj.UpdateView();
                EffectStorageContoler.SetCurrentEffect(CurrentEffectShowId);
            }
        }

        public void BuyEffect()
        {
            if (CoinsControler.BuyEffect(EffectListSO.List[CurrentEffectShowId].Cost))
            {
                EffectStorageContoler.OpenPerson(CurrentEffectShowId);
                
                StorageCoins.text = $"{CoinsControler.GetCoinsCount()}";
              
                esPageViewObj.UpdateView(CurrentEffectShowId);
                EffectStorageListComponent.MoveToNewPos(CurrentEffectShowId);
                esPanelViewObj.UpdateView();
            }
            else
            {
                AlertPanelView newAlertPanel = Instantiate(alertPanelPb, infoPanelPos);
                newAlertPanel.InitView("Cash", "You havent money");
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
                AlertPanelView newAlertPanel = Instantiate(alertPanelPb, infoPanelPos);
                newAlertPanel.InitView("Exist", "What you try to do ? Am ?");
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