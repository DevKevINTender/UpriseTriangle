using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.SessionLevel;
using UnityEngine;

public class SpawnBlockControler : MonoBehaviour
{
    public SessionLevelListScrObj SessionLevelListSO;
    public SessionLevelScrObj SessionLevelSO;
    public int CurrentLevel;
    public int NextBlockID;
    public float NextBlockTime;

    public bool isStart;
    private float timer;
    public void InitControler(int Currentlevel)
    {
        NextBlockID = 0;
        NextBlockTime = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnTime;
        isStart = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (isStart)
        {
            if (timer >= NextBlockTime)
            {
                if (NextBlockID < SessionLevelSO.SessionLevelBlockList.Count - 1)
                {
                    Instantiate(SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnBlockPb, SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnPos, Quaternion.identity);
                    NextBlockID++;
                    NextBlockTime = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnTime;
                    timer = 0;
                }
            }
        }
    }
}
