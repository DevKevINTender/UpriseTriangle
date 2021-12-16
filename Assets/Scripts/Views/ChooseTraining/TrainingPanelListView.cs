using System.Collections.Generic;
using Controlers;
using Core;
using ScriptableObjects.SessionLevel;
using ScriptableObjects.TrainLevel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.ChooseTraining
{
    public class TrainingPanelListView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private List<TrainingPanelView> LevelPanelViewList = new List<TrainingPanelView>();
        [SerializeField] private TrainingPanelView TrainingPanelViewPb;
        [SerializeField] private Transform TrainingPanelViewListTarget;
        
        private TrainingLevelListScrObj TrainingLevelListSO;
        private ChooseTrainingCore ChooseTrainingCore;
        
        [SerializeField] private Vector3 currentPos;
        
        [SerializeField] private GameObject PlayBtn;
        
        public void InitView(TrainingLevelListScrObj TrainingLevelListSO, ChooseTrainingCore ChooseTrainingCore)
        {
            currentPos = new Vector3(- TrainingLevelListSO.CurrentTrainigLevelId * 7, TrainingPanelViewListTarget.transform.position.y,TrainingPanelViewListTarget.transform.position.z);
            this.TrainingLevelListSO = TrainingLevelListSO;
            this.ChooseTrainingCore = ChooseTrainingCore;

            foreach (var item in TrainingLevelListSO.List)
            {
                TrainingPanelView newLevelPanelView = Instantiate(TrainingPanelViewPb, TrainingPanelViewListTarget);
                newLevelPanelView.transform.position = new Vector3(7 * item.LevelId, newLevelPanelView.transform.position.y, newLevelPanelView.transform.position.z);
                newLevelPanelView.InitView(item);
            }
            
            if(TrainingLevelControler.TrainingElementIsOpened(ChooseTrainingCore.CurrentTrainigLevelShowId))
            {
                PlayBtn.GetComponent<Image>().color = new Color32(255,255,255, 255);
            }
            else
            {
                PlayBtn.GetComponent<Image>().color = new Color32(36,38,46, 255);
            }
        }

        public void UpdateView(int id)
        {
            currentPos = new Vector3(- id * 7, TrainingPanelViewListTarget.transform.position.y,TrainingPanelViewListTarget.transform.position.z);
            
            if(TrainingLevelControler.TrainingElementIsOpened(ChooseTrainingCore.CurrentTrainigLevelShowId))
            {
                PlayBtn.GetComponent<Image>().color = new Color32(255,255,255, 255);
            }
            else
            {
                PlayBtn.GetComponent<Image>().color = new Color32(36,38,46, 255);
            }
        }
        
        public void FixedUpdate()
        {
            TrainingPanelViewListTarget.transform.position = Vector3.Lerp(TrainingPanelViewListTarget.transform.position, currentPos, Time.deltaTime * 15);
        }
        

        public void StartSession()
        {
            ChooseTrainingCore.StartSession();
        }

        public void BackToMenu()
        {
            ChooseTrainingCore.BackToMenu();
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
                ChooseTrainingCore.ShowPreviousLevel();
            }
            else
            {
                Debug.Log("drag 2");
                ChooseTrainingCore.ShowNextLevel();
            }
            
        }
    }
}