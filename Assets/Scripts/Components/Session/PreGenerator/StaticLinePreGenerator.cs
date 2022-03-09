using UnityEngine;
using Services;

public class StaticLinePreGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obsPb;
    [Header("Settings")]
    [SerializeField] private int obsCount;
    [SerializeField] private float obsDist;
    [SerializeField] private string PrevCommand;
    [Range(-1, 1)]
    [SerializeField] private int side;

    private Vector3 startPos;
    private GameObject obsObj;   

    [ContextMenu("CreateObsLineX")]
    public virtual void CreateObsLineX()
    {
        DestroyChilds();
        if (side == 0)
        {
            startPos = new Vector3(1, 0, 0) * obsDist * (obsCount - 1) / 2 * -1;
            side = 1;
        }     
        for (int i = 0; i < obsCount; i++)
        {
            obsObj = Instantiate(obsPb, transform);
            obsObj.transform.localPosition = startPos + new Vector3(obsDist, 0) * i * side;
        }
        PrevCommand = "CreateObsLineX";
    }

    [ContextMenu("CreateObsLineY")]
    public virtual void CreateObsLineY()
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
    public virtual void UpdateCommand()
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

    public virtual void DestroyChilds()
    {
        for (int i = this.transform.childCount; i > 0; --i)
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        startPos = Vector3.zero;
    }

}
