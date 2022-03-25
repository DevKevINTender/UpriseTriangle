using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointComponent : MonoBehaviour
{
    [SerializeField] private List<int> spawnAvailableBonusId = new List<int>();

    public bool CheckId(int id)
    {
        foreach (var item in spawnAvailableBonusId)
        {
            if (id == item) return true;
        }
        return false;
    }
}
