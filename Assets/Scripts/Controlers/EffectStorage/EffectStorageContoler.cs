using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controlers
{
    public class EffectStorageContoler
    {
        private static EffectListScrObj EffectListSO =  Resources.Load<EffectListScrObj>("ScriptableObjects/Effect/EffectListSO");
        
        public static bool ItemIsOpened(int Id)
        {
            EffectListSO.Load();
            
            foreach (var item in EffectListSO.OpenedEffectIdList)
            {
                if (Id == item) return true;
            }

            return false;
        }

        public static void OpenPerson(int Id)
        {
            EffectListSO.OpenedEffectIdList.Add(Id);
            EffectListSO.Save();
        }

        public static void SetCurrentEffect(int Id)
        {
            EffectListSO.CurrentEffectId = Id;
            EffectListSO.Save();
        }
    }
}