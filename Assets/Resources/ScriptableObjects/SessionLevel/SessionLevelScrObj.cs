using System;
using System.Collections.Generic;
using Controlers;
using UnityEngine;

namespace ScriptableObjects.SessionLevel
{
    [CreateAssetMenu(fileName = "SessionLevelSO", menuName = "ScrObj/new SessionLevelSO", order = 0)]
    public class SessionLevelScrObj : ScriptableObject
    {
        public int LevelId;
        public string MusicName;
        public string MusicCreator;
        public string MusicTime;

        public int DeadCount;
        public int CoinsCollectCount;
        public int CompletePercent;

        public int Cost;
        public GameObject SessionLevelPB;
        public List<SessionLevelBlock> SessionLevelBlockList;
    }

    [Serializable]
    public class SessionLevelBlock
    {
        public GameObject SpawnBlockPb;
        public Vector3 SpawnPos;
        public float SpawnTime;
    }
}
