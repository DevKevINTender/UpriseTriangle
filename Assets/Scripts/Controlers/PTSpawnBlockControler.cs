using ScriptableObjects.SessionLevel;
using UnityEngine;

namespace Controlers
{
    public class PTSpawnBlockControler : MonoBehaviour
    {
        [SerializeField] private SessionLevelListScrObj SessionLevelListSO;
        [SerializeField] private int sessionLevel;

        public void Init()
        {
            Instantiate(SessionLevelListSO.List[sessionLevel].SessionLevelPB);
        }
    }
}