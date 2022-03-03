using System;
using ScriptableObjects;
using UnityEngine;

namespace Components
{
    public class EffectStorageListComponent : MonoBehaviour
    {
        private EffectListScrObj EffectListSO;
        public Transform ListTarget;
        public Vector3 currentPos;

        public void InitComponent(EffectListScrObj EffectListSO)
        {
            this.EffectListSO = EffectListSO;
            GenerateList();
            currentPos = new Vector3(- EffectListSO.CurrentEffectId * 3, 0, 0);

        }

        public void GenerateList()
        {
            foreach (var item in EffectListSO.List)
            {
                Instantiate(item.EffectStoragePb, new Vector3(item.Id * 3, 0, 0), Quaternion.Euler(90, 0, 0), ListTarget) ;
            }
        }

        public void MoveToNewPos(int id)
        {
            currentPos = new Vector3(- id * 3, 0, 0);
        }

        public void Update()
        {
            ListTarget.transform.position = Vector3.Lerp(ListTarget.transform.position, currentPos, Time.deltaTime * 15);
        }

    }
}