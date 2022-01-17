using ScriptableObjects.SessionLevel;
using UnityEngine;

namespace Controlers
{
    public class PTSpawnBlockControler : MonoBehaviour
    {
        [SerializeField] private SessionLevelListScrObj SessionLevelListSO;

        public void Init()
        {
            Instantiate(SessionLevelListSO.List[0].SessionLevelPB);
        }
    }
}