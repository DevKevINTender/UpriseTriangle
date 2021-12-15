using ScriptableObjects.SessionLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ChooseLevel
{
    public class LevelPanelView : MonoBehaviour
    {
        private SessionLevelScrObj SessionLevelSO;

        public Text Level;
        public Text MusicName;
        public Text MusicCreator;
        public Text MusicTime;
        
        public Text DeadCount;
        public Text CoinsCollectCount;
        public Text CompletePercent;
        
        
        public void InitView(SessionLevelScrObj SessionLevelSO)
        {
            this.SessionLevelSO = SessionLevelSO;

            Level.text = $"{SessionLevelSO.LevelId + 1}";
            MusicName.text = $"{SessionLevelSO.MusicName}";
            MusicCreator.text = $"{SessionLevelSO.MusicCreator}";
            MusicTime.text = $"{SessionLevelSO.MusicTime}";
            
            DeadCount.text = $"{SessionLevelSO.DeadCount}";
            CoinsCollectCount.text = $"{SessionLevelSO.CoinsCollectCount}";
            CompletePercent.text = $"{SessionLevelSO.CompletePercent}";
        }
    }
}