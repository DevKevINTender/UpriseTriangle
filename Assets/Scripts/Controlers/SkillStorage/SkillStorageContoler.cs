using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controlers
{
    public class SkillStorageContoler
    {
        private static SkillListScrObj SkillListSO =  Resources.Load<SkillListScrObj>("ScriptableObjects/Skills/SkillListSO");
        
        public static bool ItemIsOpened(int id)
        {
            SkillListSO.Load();
            if (SkillListSO.List[id].RequiredSegments <= SkillListSO.CurrentSegmentListCount[id]) return true;
            return false;
        }

        public static void AddSegmentToSkill(int Id)
        {
            SkillListSO.CurrentSegmentListCount[Id]++;
            SkillListSO.Save();
        }

        public static void SetCurrentSkill(int Id)
        {
            SkillListSO.CurrentSkillId = Id;
            SkillListSO.Save();
        }
        
        public static int GetCurrentSkill()
        {
           return SkillListSO.CurrentSkillId;
        }

        public static SkillScrObj GetSkillById(int Id)
        {
            SkillListSO.Load();
            return SkillListSO.List[Id];
        }
        public static  List<SkillScrObj> GetSkillItemForPage(int pageId)
        {
            SkillListSO.Load();
            List<SkillScrObj> list = new List<SkillScrObj>();
            for (int i = 0 + 4 * pageId; i < 4 + 4 * pageId; i++)
            {
                if (i < SkillListSO.List.Count)
                {
                    list.Add(SkillListSO.List[i]);
                }
            }
           
            return list;
        }
        
        public static  List<SkillScrObj> GetNotOpenedSkills()
        {
            SkillListSO.Load();
            List<SkillScrObj> list = new List<SkillScrObj>();
            foreach (var item in SkillListSO.List)
            {
                if (item.RequiredSegments > SkillListSO.CurrentSegmentListCount[item.Id]) list.Add(item);
            }
            return list;
        }
    }
}