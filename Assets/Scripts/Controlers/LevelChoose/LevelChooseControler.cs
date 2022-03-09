using ScriptableObjects;
using ScriptableObjects.SessionLevel;
using UnityEngine;

namespace Controlers
{
    public class LevelChooseControler
    {
        private static SessionLevelListScrObj SessionLevelListSO = Resources.Load<SessionLevelListScrObj>("ScriptableObjects/SessionLevel/SessionLevelListSO");
        
        public static bool LevelIsOpened(int Id)
        {
            SessionLevelListSO.Load();
            
            foreach (var item in SessionLevelListSO.OpenedSessionLevelIdList)
            {
                if (Id == item) return true;
            }

            return false;
        }

        public static void OpenLevel(int Id)
        {
            SessionLevelListSO.OpenedSessionLevelIdList.Add(Id);
            SessionLevelListSO.Save();
        }

        public static void SetCurrentLevel(int Id)
        {
            SessionLevelListSO.CurrentSessionLevelId = Id;
            SessionLevelListSO.Save();
        }
    }
}