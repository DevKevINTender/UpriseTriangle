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
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath+"/"+SavePath));
            bf.Serialize(file, saveData);
            file.Close();
        }
        
        [ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("loaded");
            if (File.Exists(string.Concat(Application.persistentDataPath+"/"+SavePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath+"/"+SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
            }
        }
    }
}