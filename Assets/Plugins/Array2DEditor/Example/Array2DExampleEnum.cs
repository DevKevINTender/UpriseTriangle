using Services;
using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DExampleEnum : Array2D<GridEnum>
    {
        [SerializeField]
        CellRowExampleEnum[] cells = new CellRowExampleEnum[Consts.defaultGridSize];

        protected override CellRow<GridEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
    
    [System.Serializable]
    public class CellRowExampleEnum : CellRow<GridEnum> { }

}
