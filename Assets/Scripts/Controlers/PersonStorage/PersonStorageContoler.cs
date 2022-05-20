using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controlers
{
    public class PersonStorageContoler
    {
        private static PersonListScrObj PersonListSO =  Resources.Load<PersonListScrObj>("ScriptableObjects/Person/PersonListSO");
        
        public static bool ItemIsOpened(int id)
        {
            PersonListSO.Load();
            if (PersonListSO.List[id].RequiredSegments <= PersonListSO.CurrentSegmentListCount[id]) return true;
            return false;
        }

        public static void AddSegmentToPerson(int Id)
        {
            PersonListSO.CurrentSegmentListCount[Id]++;
            PersonListSO.Save();
        }

        public static void SetCurrentPerson(int Id)
        {
            PersonListSO.CurrentPersonId = Id;
            PersonListSO.Save();
        }
        
        public static int GetCurrentPerson()
        {
           return PersonListSO.CurrentPersonId;
        }

        public static PersonScrObj GetPersonById(int Id)
        {
            PersonListSO.Load();
            return PersonListSO.List[Id];
        }
        public static  List<PersonScrObj> GetPersonItemForPage(int pageId)
        {
            PersonListSO.Load();
            List<PersonScrObj> list = new List<PersonScrObj>();
            for (int i = 0 + 9 * pageId; i < 9 + 9 * pageId; i++)
            {
                if (i < PersonListSO.List.Count)
                {
                    list.Add(PersonListSO.List[i]);
                }
            }
           
            return list;
        }
        
        public static  List<PersonScrObj> GetNotOpenedPersons()
        {
            PersonListSO.Load();
            List<PersonScrObj> list = new List<PersonScrObj>();
            foreach (var item in PersonListSO.List)
            {
                if (item.RequiredSegments > PersonListSO.CurrentSegmentListCount[item.Id]) list.Add(item);
            }
            return list;
        }
    }
}