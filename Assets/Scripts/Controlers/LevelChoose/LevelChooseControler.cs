using System.Collections.Generic;
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

        public static List<SessionLevelScrObj> GetSessionLevelsFromPage(int pageId)
        {
            SessionLevelListSO.Load();
            List<SessionLevelScrObj> list = new List<SessionLevelScrObj>();
            for (int i = 0 + 3 * pageId; i <3 + 3 * pageId; i++)
            {
                if (i < SessionLevelListSO.List.Count)
                {
                    list.Add(SessionLevelListSO.List[i]);
                }
            }
           
            return list;
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
        public static int GetCurrentLevel()
        {
            SessionLevelListSO.Load();
            return SessionLevelListSO.CurrentSessionLevelId;
        }

        public static SessionLevelScrObj GetLevelById(int id)
        {
            SessionLevelListSO.Load();
            return SessionLevelListSO.List[id];
        }

        public static int GetSessionAttempCount(int id)
        {
            SessionLevelListSO.Load();
            return SessionLevelListSO.List[id].AttempCount;
        }

        public static int GetSessionWinReward(int id)
        {
            SessionLevelListSO.Load();
            return SessionLevelListSO.List[id].WinReward;
        }
        public static void SetSessionAttempCount(int id, int count)
        {
            SessionLevelListSO.List[id].AttempCount = count;
            SessionLevelListSO.Save();
        }
    }
}