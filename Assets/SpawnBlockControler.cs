using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.SessionLevel;
using UnityEngine;

public class SpawnBlockControler : MonoBehaviour
{
    public SessionLevelListScrObj SessionLevelListSO;
    public SessionLevelScrObj SessionLevelSO;
    public GameObject winerPanel;
    public int CurrentLevel;
    public int NextBlockID;
    public float NextBlockTime;

    public bool isStart;
    private float timer;

    private GameObject spawningObj;
    private Vector3 spawningPos;
    public void InitControler(int Currentlevel)
    {
        NextBlockID = 0;
        NextBlockTime = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnTime;
        isStart = true;
    }

    public void SetWinnerPanelToFinish(GameObject _ChekingObj)
    {
        if (_ChekingObj.GetComponent<FinishLineComponent>() != null)
            _ChekingObj.GetComponent<FinishLineComponent>().SetWinPanel(winerPanel);
    }

    public void SpawnNewElement()
    {
        spawningObj = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnBlockPb;
        spawningPos = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnPos;
        GameObject spawnedObj = Instantiate(spawningObj, spawningPos, Quaternion.identity);
        SetWinnerPanelToFinish(spawnedObj);
        NextBlockID++;
        NextBlockTime = SessionLevelSO.SessionLevelBlockList[NextBlockID].SpawnTime;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (isStart)
        {
            if (timer >= NextBlockTime)
            {
                if (NextBlockID <= SessionLevelSO.SessionLevelBlockList.Count)
                {
                    SpawnNewElement();
                    timer = 0;
                }
            }
        }
    }
}
