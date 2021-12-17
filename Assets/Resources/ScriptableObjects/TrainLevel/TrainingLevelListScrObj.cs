using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ScriptableObjects.TrainLevel
{
    [CreateAssetMenu(fileName = "TrainingLevelListSO", menuName = "ScrObj/new TrainingLevelListSO", order = 0)]
    public class TrainingLevelListScrObj : ScriptableObject
    {
        public string SavePath;
        //Save
        public int CurrentTrainigLevelId;
        public List<int> OpenedTrainingLevelIdList = new List<int>();
        public List<TrainingLevelScrObj> List = new List<TrainingLevelScrObj>();
        
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