using UnityEngine;
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
    private Vector3 startPos;
    private GameObject obsObj;
    private float resolution;

    [ContextMenu("CreateObsLineX")]
    public void CreateObsLineX()
    {
        resolution = (MainCamera.pixelHeight / (2 * MainCamera.orthographicSize)) / 100;
        DestroyChilds();
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.transform.localPosition = startPos + new Vector3(obsDist, 0) * i * side;
            obsObj.transform.localPosition += new Vector3(0, resolution + 0.3f);
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

    public void DestroyChilds()
    {
        for (int i = this.transform.childCount; i > 0; --i)
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        startPos = Vector3.zero;
    }

}
