using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineService : MonoBehaviour
{
    [SerializeField] private GameObject TimeElementPB;
    [SerializeField] private float GameSpeed;
    [SerializeField] private int MusicTime;    
    [SerializeField] private List<GameObject> TimeList = new List<GameObject>();

    [ContextMenu("CreateTimer")]
    public void CreateTime()
    {
        for (int i = 0; i < MusicTime / 0.5f; i++)
        {
            TimeList.Add(Instantiate(TimeElementPB, new Vector3(-3.5f, 5 + (0.5f * i * GameSpeed), 0), Quaternion.identity, transform));
            TimeList[i].GetComponent<TextMesh>().text = $"T {0.5f * i}";
        }
       
    }

    [ContextMenu("DeleteTimer")]
    public void DeleteTimer()
    {
        foreach (var item in TimeList)
        {
            DestroyImmediate(item);
        }
        TimeList.Clear();
    }
    
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
