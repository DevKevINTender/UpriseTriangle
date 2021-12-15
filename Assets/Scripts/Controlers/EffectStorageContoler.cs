using ScriptableObjects;
using UnityEngine;

namespace Controlers
{
    public class EffectStorageContoler
    {
        private static int CurrentEffectID = PlayerPrefs.GetInt("CurrentEffectID", 0);
        private static EffectListScrObj EffectListSO =  Resources.Load<EffectListScrObj>("ScriptableObjects/Effect/EffectListSO");
        
        public static int GetCurrentEffectID()
        {
            return CurrentEffectID;
        }

        public static bool ItemIsOpened(int Id)
        {
            EffectListSO.Load();
            
            foreach (var item in EffectListSO.OpenedEffectIdList)
            {
                if (Id == item) return true;
            }

            return false;
        }
    }
}