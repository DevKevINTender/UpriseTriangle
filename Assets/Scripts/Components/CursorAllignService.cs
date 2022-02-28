using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAllignService : MonoBehaviour
{

    [SerializeField] private GameObject obsPb;
    [Header("Settings")]
    [SerializeField] private int obsCount;
    [SerializeField] private float obsDist;
    [SerializeField] private string PrevCommand;
    [Range(-1, 1)]
    [SerializeField] private int side;
    [Header("Time")]
    [SerializeField] private float timeStart;
    [SerializeField] private float timeStep;

    private Vector3 startPos;
    private GameObject obsObj;
    private CursorObstacleComponent cursor;
    [SerializeField] private ElevatorComponent elevator;
    private List<float> savedValues = new List<float>();

    public void Start()
    {
        elevator.AddToList(transform.GetComponent<CursorAllignService>());
    }



    [ContextMenu("CreateObsLineX")]
    public void CreateObsLineX()
    {
        DestroyChilds();
        elevator = transform.parent.GetComponent<ElevatorComponent>();
        startPos = new Vector3(1, 0, 0) * obsDist * (obsCount - 1) / 2 * -side;
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.transform.localPosition = startPos + new Vector3(obsDist, 0) * i * side;
            obsObj.transform.rotation = Quaternion.Euler(0, 0, 90 * -side);
            obsObj.transform.position += new Vector3(0, 5.3f * side);
            LoadOldValues(GetCursorComponent(i), i);
        }
        PrevCommand = "CreateObsLineX";
    }

    [ContextMenu("CreateObsLineY")]
    public void CreateObsLineY()
    {
        DestroyChilds();
        elevator = transform.parent.GetComponent<ElevatorComponent>();
        startPos = new Vector3(0, 1, 0) * obsDist * (obsCount - 1) / 2 * -side;
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.transform.localPosition = startPos + new Vector3(0, obsDist) * i * side;
            obsObj.transform.rotation = Quaternion.Euler(0, 0, GetRotation());
            obsObj.transform.position += new Vector3(3.2f * side, 0);
            LoadOldValues(GetCursorComponent(i), i);
        }
        PrevCommand = "CreateObsLineY";
    }

    [ContextMenu("AutoTiming")]
    public void AutoTiming()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cursor = GetCursorComponent(i);
            cursor.startDelay = timeStart + i * timeStep;
            cursor.target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    [ContextMenu("UpdateCommand")]
    public void UpdateCommand()
    {
        if (PrevCommand == "CreateObsLineX")
        {
            CreateObsLineX();
        }
        else
        {
            CreateObsLineY();
        }
    }

    public void ActivateCursors()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cursor = GetCursorComponent(i);
            cursor.Activate();
        }
    }

    public void SaveOldValues(CursorObstacleComponent cursor)
    {
        savedValues.Add(cursor.startDelay);
    }

    public void LoadOldValues(CursorObstacleComponent cursor, int i)
    {
        AutoTiming();
        if (savedValues.Count > 0 && i < savedValues.Count)
        { 
            cursor.startDelay = savedValues[i];
            cursor.target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public CursorObstacleComponent GetCursorComponent(int i)
    {
        return transform.GetChild(i).GetComponent<CursorObstacleComponent>(); 
    }

    public int GetRotation()
    {
        if (side == -1) return 0;
        return 180;      
    }

    public void DestroyChilds()
    {
        if (savedValues.Count > 0)
        {
            savedValues.Clear();
        }
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount; i > 0; --i)
            {
                SaveOldValues(GetCursorComponent(0));
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}