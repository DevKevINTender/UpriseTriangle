using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ScriptableObjects.SessionLevel
{
    [CreateAssetMenu(fileName = "SessionLevelListSO", menuName = "ScrObj/new SessionLevelListSO", order = 0)]
    public class SessionLevelListScrObj : ScriptableObject
    {
        public string SavePath;
        //Save
        public int CurrentSessionLevelId;
        public List<int> OpenedSessionLevelIdList = new List<int>();
        public List<SessionLevelScrObj> List = new List<SessionLevelScrObj>();
        
        [ContextMenu("Save")]
        public void Save()
        {
            Debug.Log("saved");
            
            SessionLevelListSave newSessionLevelListSave = new SessionLevelListSave();
            newSessionLevelListSave.CurrentSessionLevelId = CurrentSessionLevelId;
            newSessionLevelListSave.OpenedSessionLevelIdList = OpenedSessionLevelIdList;
            newSessionLevelListSave.List = new List<SessionLevelSave>();
            
            foreach (var item in List)
            {
                SessionLevelSave newSessionLevelSave = new SessionLevelSave();
                newSessionLevelSave.CompletePercent = item.CompletePercent;
                newSessionLevelSave.DeadCount = item.DeadCount;
                newSessionLevelSave.CoinsCollectCount = item.CoinsCollectCount;
                newSessionLevelListSave.List.Add(newSessionLevelSave);
            }
            
            string saveData = JsonUtility.ToJson(newSessionLevelListSave, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath+"/"+SavePath));
            bf.Serialize(file, saveData);
            file.Close();
        }
        
        [ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("loaded");
            SessionLevelListSave newSessionLevelListSave = new SessionLevelListSave();
            if (File.Exists(string.Concat(Application.persistentDataPath,"/",SavePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath,"/",SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), newSessionLevelListSave);
               
                
                CurrentSessionLevelId = newSessionLevelListSave.CurrentSessionLevelId;
                OpenedSessionLevelIdList = newSessionLevelListSave.OpenedSessionLevelIdList;

                for (int i = 0; i < newSessionLevelListSave.List.Count; i++)
                {
                    List[i].Id = i;
                    List[i].DeadCount = newSessionLevelListSave.List[i].DeadCount;
                    List[i].CompletePercent = newSessionLevelListSave.List[i].CompletePercent;
                    List[i].CoinsCollectCount = newSessionLevelListSave.List[i].CoinsCollectCount;
                }

                file.Close();
            }
        }
    }
    
    public class SessionLevelListSave
    {
        public int CurrentSessionLevelId;
        public List<int> OpenedSessionLevelIdList = new List<int>();
        public List<SessionLevelSave> List;
    }

    [Serializable]
    public class SessionLevelSave
    {
        public int DeadCount;
        public int CoinsCollectCount;
        public int CompletePercent;
    }
}