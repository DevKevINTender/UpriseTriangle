using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SkillListSO", menuName = "ScrObj/new SkillListSO", order = 0)]
    public class SkillListScrObj : ScriptableObject
    {
        public string SavePath;
        // Save
        public int CurrentSkillId;
        public int CurrentPageId;
        public List<int> CurrentSegmentListCount = new List<int>();
        //Not Save
        public List<SkillScrObj> List = new List<SkillScrObj>();

        [ContextMenu("Save")]
        public void Save()
        {
            SkillListScrObjSave newSaveData = new SkillListScrObjSave();
            newSaveData.CurrentSkillId = CurrentSkillId;
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
            SkillListScrObjSave newSaveData = new SkillListScrObjSave();
            if(File.Exists(string.Concat(Application.persistentDataPath,"/", SavePath))){
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath,"/", SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), newSaveData);
                
                CurrentSkillId = newSaveData.CurrentSkillId;
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
    
    public class SkillListScrObjSave
    {
        public int CurrentSkillId;
        public int CurrentPageId;
        public List<int> CurrentSegmentListCount;
    }
}