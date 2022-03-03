using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmLineService : MonoBehaviour
{
    [SerializeField] private float bpm;
    private float secondPerBpm;
    private float step;
    [SerializeField] private float starttime;
    private float startPos;
    [SerializeField] private float temp;
    [SerializeField] private GameObject TimeElementPB;
    [SerializeField] private float GameSpeed;
    [SerializeField] private int MusicTime;
    [SerializeField] private List<GameObject> TimeList = new List<GameObject>();

    [ContextMenu("CreateTimer")]
    public void CreateTime()
    {
        secondPerBpm = 60 / bpm;
        step = secondPerBpm * GameSpeed * temp;
        startPos = 5 + starttime * 5 - 0.25f;
        for (int i = 0; i < MusicTime / 0.5f; i++)
        {
            TimeList.Add(Instantiate(TimeElementPB, new Vector3(4.5f, startPos + (i * step), 0), Quaternion.identity, transform));
            TimeList[i].GetComponent<TextMesh>().text = temp.ToString();
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
