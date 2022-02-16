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
    [Range(-1, 1)]
    [SerializeField] private int side;
    [SerializeField] private Vector3 startPos = Vector3.zero;
    private ElevatorComponent elevatorComponent;
    private GameObject obsObj;
    private float resolution;
    private List<SavedValues> savedValues = new List<SavedValues>();
    private struct SavedValues
    {
        public float TimerStart { get; set; }
        public float TimeStartChangeColor { get; set; }
        public float ColorChangeTime { get; set; }
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
            oldSavedValues.TimerStart = pTWayObsComponent.TimerStart;
            oldSavedValues.TimeStartChangeColor = pTWayObsComponent.timeStartChangeColor;
            oldSavedValues.ColorChangeTime = pTWayObsComponent.colorChangeTime;
            savedValues.Add(oldSavedValues);
        }
    }

    public void LoadOldWayValues(int i, GameObject wayObj)
    {
        if (savedValues.Count > i)
        {
            PTWayObsComponent pTWayObsComponent = wayObj.GetComponent<PTWayObsComponent>();
            pTWayObsComponent.TimerStart = savedValues[i].TimerStart;
            pTWayObsComponent.timeStartChangeColor = savedValues[i].TimeStartChangeColor;
            pTWayObsComponent.colorChangeTime = savedValues[i].ColorChangeTime;
        }
    }

    public void SetElevatorComponent()
    {
        elevatorComponent = transform.GetComponentInParent<ElevatorComponent>();
    }

    [ContextMenu("CreateObsLineX")]
    public void CreateObsLineX()
    {
        resolution = (MainCamera.pixelHeight / (2 * MainCamera.orthographicSize)) / 100;
        DestroyChilds();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.name = (1 + i).ToString();
            obsObj.transform.localPosition = startPos + new Vector3(obsDist, 0) * i * side;
            obsObj.transform.localPosition += new Vector3(0, resolution + 0.3f);
            LoadOldWayValues(i, obsObj);
        }
        PrevCommand = "CreateObsLineX";
    }

    [ContextMenu("CreateObsLineY")]
    public void CreateObsLineY()
    {
        DestroyChilds();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.transform.localPosition = startPos + new Vector3(0, obsDist) * i * side;
        }
        PrevCommand = "CreateObsLineY";
    }

    [ContextMenu("UpdateCommand")]
    public void UpdateCommand()
    {
        if (PrevCommand == "CreateObsLineX")
        {
            CreateObsLineX();
        }
        if (PrevCommand == "CreateObsLineY")
        {
            CreateObsLineY();
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
