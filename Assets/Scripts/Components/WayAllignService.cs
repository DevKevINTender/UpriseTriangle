using UnityEngine;
using Components.Session;
using System.Collections.Generic;
using Services;

public class WayAllignService : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject obsPb;
    [Header("Settings")]
    [SerializeField] private int obsCount;
    [SerializeField] private float obsDist;
    [SerializeField] private string PrevCommand;

    [SerializeField] private float startStep;
    [SerializeField] private float appearStep;
    
    [Range(-1, 1)]
    [SerializeField] private int side;
    [SerializeField] private Vector3 startPos = Vector3.zero;
    private ElevatorComponent elevatorComponent;
    private GameObject obsObj;
    private float resolution;
    private List<SavedValues> savedValues = new List<SavedValues>();
    private struct SavedValues
    {
        public float appeartime { get; set; }
        public float obstacleSpeed { get; set; }
        public float Target { get; set; }
    }

    public void Awake()
    {      
        SetElevatorComponent();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            AddToElevatorList(transform.GetChild(i).GetComponent<PTWayObsComponent>());
        }
    }

    public void SaveOldWayValues()
    {
        if(savedValues.Count > 0) savedValues.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            PTWayObsComponent pTWayObsComponent = transform.GetChild(i).GetComponent<PTWayObsComponent>();
            SavedValues oldSavedValues = new SavedValues();
            oldSavedValues.appeartime = pTWayObsComponent.appeartime;
            oldSavedValues.obstacleSpeed = pTWayObsComponent.obstacleSpeed;
            oldSavedValues.Target = pTWayObsComponent.Target;
            savedValues.Add(oldSavedValues);
        }
    }

    public void LoadOldWayValues(int i, GameObject wayObj)
    {
        if (savedValues.Count > i)
        {
            PTWayObsComponent pTWayObsComponent = wayObj.GetComponent<PTWayObsComponent>();
            pTWayObsComponent.appeartime = savedValues[i].appeartime;
            pTWayObsComponent.Target = savedValues[i].Target;
            pTWayObsComponent.obstacleSpeed = savedValues[i].obstacleSpeed;
        }
    }

    public void SetElevatorComponent()
    {
        elevatorComponent = transform.GetComponentInParent<ElevatorComponent>();
    }

    [ContextMenu("CreateObsLineX")]
    public void CreateObsLineX()
    {
        DestroyChilds();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.name = (1 + i).ToString();
            obsObj.transform.localPosition = startPos + new Vector3(obsDist, 0) * i * side;
            obsObj.transform.localPosition += new Vector3(0, 5f);
            LoadOldWayValues(i, obsObj);
        }
        PrevCommand = "CreateObsLineX";
    }

    [ContextMenu("AutoAppearTime")]
    public void AutoAppearTime()
    {
        for (int i = 0; i < obsCount; i++)
        {
            PTWayObsComponent pTWayObsComponent = transform.GetChild(i).GetComponent<PTWayObsComponent>();
            pTWayObsComponent.appeartime = startStep + appearStep * i;
        }
    }

    [ContextMenu("UpdateCommand")]
    public void UpdateCommand()
    {
        if (PrevCommand == "CreateObsLineX")
        {
            CreateObsLineX();
        }
    }

    public void AddToElevatorList(PTWayObsComponent _pTWayObsComponent)
    {
        elevatorComponent.AddToList(_pTWayObsComponent);
    }

    public void DestroyChilds()
    {
        SaveOldWayValues();
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
    }

}
