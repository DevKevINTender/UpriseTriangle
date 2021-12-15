using UnityEngine;

namespace ScriptableObjects.SessionLevel
{
    [CreateAssetMenu(fileName = "SessionLevelSO", menuName = "ScrObj/new SessionLevelSO", order = 0)]
    public class SessionLevelScrObj : ScriptableObject
    {
        public int Level;
        public int MusicName;
        public int MusicCreator;
        public int StingTime;

        public int DeadCount;
        public int CoinsCollectCount;
        public int CompletePercent;
        
        public int IsOpened;
    }
}