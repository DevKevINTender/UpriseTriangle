using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TimeLineService : MonoBehaviour
{
    [SerializeField]
    private GameObject TimeElementPB;
    [SerializeField]
    private List<GameObject> TimeList = new List<GameObject>();

    [ContextMenu("Load")]
    public void CreateTime()
    {
        
    }
    
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeList.Count < 10)
        {
            TimeList.Add(Instantiate(TimeElementPB));
        }
    }
}
