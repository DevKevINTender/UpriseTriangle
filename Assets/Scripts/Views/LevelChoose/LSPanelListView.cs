using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LSPanelListView : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private List<LSPanelItemView> LevelPanelViewList = new List<LSPanelItemView>();
        [SerializeField] private LSPanelItemView lsPanelItemViewPb;
        [SerializeField] private Transform LevelPanelViewListTarget;
        
        private SessionLevelListScrObj SessionLevelListScrObj;
        private ChooseLevelCore ChooseLevelCore;
        
        [SerializeField] private Vector3 currentPos;

        [SerializeField] private GameObject BuyBtn;
        [SerializeField] private Text LevelCost;
        [SerializeField] private GameObject PlayBtn;
        
        public void InitView(SessionLevelListScrObj SessionLevelListScrObj, ChooseLevelCore ChooseLevelCore)
        {
            currentPos = new Vector3(- SessionLevelListScrObj.CurrentSessionLevelId * 7, LevelPanelViewListTarget.transform.position.y,LevelPanelViewListTarget.transform.position.z);
            this.SessionLevelListScrObj = SessionLevelListScrObj;
            this.ChooseLevelCore = ChooseLevelCore;

            foreach (var item in SessionLevelListScrObj.List)
            {
                LSPanelItemView newLsPanelItemView = Instantiate(lsPanelItemViewPb, LevelPanelViewListTarget);
                newLsPanelItemView.transform.position = new Vector3(7 * item.LevelId, newLsPanelItemView.transform.position.y, newLsPanelItemView.transform.position.z);
                newLsPanelItemView.InitView(item);
            }
            
            if(LevelChooseControler.LevelIsOpened(ChooseLevelCore.CurrentLevelShowId))
            {
                BuyBtn.SetActive(false);
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                LevelCost.text = $"{SessionLevelListScrObj.List[ChooseLevelCore.CurrentLevelShowId].Cost}";
                BuyBtn.SetActive(true);
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
            }
        }

        public void UpdateView(int id)
        {
            currentPos = new Vector3(- id * 7, LevelPanelViewListTarget.transform.position.y,LevelPanelViewListTarget.transform.position.z);
            
            if(LevelChooseControler.LevelIsOpened(ChooseLevelCore.CurrentLevelShowId))
            {
                BuyBtn.SetActive(false);
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(255, 255, 255, 255);   
            }
            else
            {
                LevelCost.text = $"{SessionLevelListScrObj.List[ChooseLevelCore.CurrentLevelShowId].Cost}";
                BuyBtn.SetActive(true);
                PlayBtn.transform.GetChild(0).GetComponent<Text>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(1).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
                PlayBtn.transform.GetChild(2).GetComponent<Image>().color = new Color32(36, 38, 46, 255);
            }
        }

        public void Update()
        {
            LevelPanelViewListTarget.transform.position = Vector3.Lerp(LevelPanelViewListTarget.transform.position, currentPos, Time.deltaTime * 15);
        }
        
        public void BuyLevel()
        {
            ChooseLevelCore.BuyLevel();
        }

        public void StartSession()
        {
            ChooseLevelCore.StartSession();
        }

        public void BackToMenu()
        {
            ChooseLevelCore.BackToMenu();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("drag");
            if (eventData.delta.x > 0)
            {
                Debug.Log("drag 1");
                ChooseLevelCore.ShowPreviousLevel();
            }
            else
            {
                Debug.Log("drag 2");
                ChooseLevelCore.ShowNextLevel();
            }
            
        }
    }
}