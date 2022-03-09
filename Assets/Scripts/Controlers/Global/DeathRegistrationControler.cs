using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Controlers
{
    public class DeathRegistrationControler
    {
        private string SavePath = "DeathRecords.json";
        public void AddNewRecord()
        {
            DeathRecordList deathRecordList = new DeathRecordList();
            deathRecordList = GetRecordList();

            if (deathRecordList.List.Count >= 10)
            {
                deathRecordList.List.RemoveAt(0);
            }
            
            string saveData = JsonUtility.ToJson(deathRecordList, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath+"/"+SavePath));
            bf.Serialize(file, deathRecordList);
            file.Close();
        }

        private bool Check()
        {
            return true;
        }

        public DeathRecordList GetRecordList()
        {
            DeathRecordList loadDeathRecordList = new DeathRecordList();
            
            if (File.Exists(string.Concat(Application.persistentDataPath, "/", SavePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, "/", SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), loadDeathRecordList);

                file.Close();
            }
            return loadDeathRecordList;
        }
    }

    public class DeathRecordList
    {
        public List<DeathRecord> List = new List<DeathRecord>();
    }
    public class DeathRecord
    {
        public DateTime time;
        public float percentComplete;
    }
}