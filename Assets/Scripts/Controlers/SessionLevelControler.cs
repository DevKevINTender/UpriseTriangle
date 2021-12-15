using UnityEngine;

namespace Controlers
{
    public class SessionLevelControler
    {
        private static int CurrentLevelID = PlayerPrefs.GetInt("CurrentLevelID", 0);
        
        public static int GetCurrentLevel()
        {
            return CurrentLevelID;
        }
    }
}