using System;
using System.Collections.Generic;
using Array2DEditor;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] public List<Array2DBool> listBool = new List<Array2DBool>();
    private Array2DBool list;

    private void Start()
    {
        
        foreach (var item in listBool[0].GetCells())
       {
           
       }
    
    }
}

[Serializable]
public class Level
{
    [SerializeField] public List<bool> listBool = new List<bool>();
}