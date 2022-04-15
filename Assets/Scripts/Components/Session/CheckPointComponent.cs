using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CheckPointComponent : MonoBehaviour
{
    [SerializeField] private List<int> spawnAvailableBonusId = new List<int>();
    [SerializeField] private Transform rightLine;
    [SerializeField] private Transform leftLine;
    public bool CheckId(int id)
    {
        foreach (var item in spawnAvailableBonusId)
        {
            if (id == item) return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BonusCollectorComponent>())
        {
            rightLine.DOPunchPosition(Vector3.right * 2, 1, 2, 0.1f);
            leftLine.DOPunchPosition(Vector3.left * 2, 1, 2, 0.1f);
        }
    }
}
