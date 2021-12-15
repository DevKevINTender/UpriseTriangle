using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EffectListSO", menuName = "ScrObj/new EffectListSO", order = 0)]
    public class EffectListScrObj : ScriptableObject
    {
        public string SavePath;
        // Save
        public int CurrentEffectId;
        public List<int> OpenedEffectIdList = new List<int>();
        //Not Save
        public List<EffectScrObj> List = new List<EffectScrObj>();

        [ContextMenu("Save")]
        public void Save()
        {
            EffectListScrObjSave newSaveData = new EffectListScrObjSave();
            newSaveData.CurrentEffectId = CurrentEffectId;
            newSaveData.OpenedEffectIdList = OpenedEffectIdList;

            string saveData = JsonUtility.ToJson(newSaveData, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/", SavePath));
            bf.Serialize(file, saveData);
            file.Close();
        }
        
        [ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("EffectListLoad");
            EffectListScrObjSave newSaveData = new EffectListScrObjSave();
            if(File.Exists(string.Concat(Application.persistentDataPath,"/", SavePath))){
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath,"/", SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), newSaveData);
                
                CurrentEffectId = newSaveData.CurrentEffectId;
                OpenedEffectIdList = newSaveData.OpenedEffectIdList;

                file.Close();
            }
            
        }    
    }
    public class EffectListScrObjSave
    {
        public int CurrentEffectId;
        public List<int> OpenedEffectIdList;
    }
}