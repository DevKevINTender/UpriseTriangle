using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Controlers
{
    public static class DeathRegistrationControler
    {
        private static  string SavePath = "DeathRecords.json";
        public static void AddNewRecord(DateTime time, int checkPoint)
        {
            DeathRecordList deathRecordList = new DeathRecordList();
            DeathRecordList getedRecordList = GetRecordList();
            deathRecordList = getedRecordList == null ? deathRecordList : getedRecordList;
            if (deathRecordList.List.Count >= 5)
            {
                deathRecordList.List.RemoveAt(0);
            }

            DeathRecord newRecord = new DeathRecord();
            newRecord.time = time.ToFileTimeUtc();
            newRecord.checkPoint = checkPoint;
            
            deathRecordList.List.Add(newRecord);
            string saveData = JsonUtility.ToJson(deathRecordList, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath+"/"+SavePath));
            bf.Serialize(file, saveData);
            file.Close();
        }

        private static bool Check()
        {
            return true;
        }

        public static DeathRecordList GetRecordList()
        {
            DeathRecordList loadDeathRecordList = new DeathRecordList();
            
            if (File.Exists(string.Concat(Application.persistentDataPath, "/", SavePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, "/", SavePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), loadDeathRecordList);

                file.Close();
                return loadDeathRecordList;
            }

            return null;

        }
    }

    [Serializable]
    public class DeathRecordList
    {
        public List<DeathRecord> List = new List<DeathRecord>();
    }
    [Serializable]
    public class DeathRecord
    {
        public long  time;
        public int checkPoint;
    }
}