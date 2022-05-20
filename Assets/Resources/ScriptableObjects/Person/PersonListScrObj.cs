using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PersontListSO", menuName = "ScrObj/new PersontListSO", order = 0)]
    public class PersonListScrObj : ScriptableObject
    {
        public string SavePath;
        // Save
        public int CurrentPersonId;
        public int CurrentPageId;
        public List<int> CurrentSegmentListCount = new List<int>();
        //Not Save
        public List<PersonScrObj> List = new List<PersonScrObj>();

        [ContextMenu("Save")]
        public void Save()
        {
            PersonListScrObjSave newSaveData = new PersonListScrObjSave();
            newSaveData.CurrentPersonId = CurrentPersonId;
            newSaveData.CurrentPageId = CurrentPageId;
            newSaveData.CurrentSegmentListCount = CurrentSegmentListCount;

            string saveData = JsonUtility.ToJson(newSaveData, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/", SavePath));
            bf.Serialize(file, saveData);
            file.Close();
        }
        
        [ContextMenu("Load")]
        public void Load()
        {
            PersonListScrObjSave newSaveData = new PersonListScrObjSave();
            if(File.Exists(string.Concat(Application.persistentDataPath,"/", SavePath))){
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath,"/", SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), newSaveData);
                
                CurrentPersonId = newSaveData.CurrentPersonId;
                CurrentPageId = newSaveData.CurrentPageId;
                CurrentSegmentListCount = newSaveData.CurrentSegmentListCount;
                for (int i = 0; i < List.Count; i++)
                {
                    List[i].Id = i;
                    List[i].CurrentSegment = CurrentSegmentListCount[i];
                }

                file.Close();
            }
            
        }    
    }
    
    public class PersonListScrObjSave
    {
        public int CurrentPersonId;
        public int CurrentPageId;
        public List<int> CurrentSegmentListCount;
    }
}