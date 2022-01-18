using UnityEngine;

    public class PTMovePointComponent : MonoBehaviour
    {
        [SerializeField] private PrototypeSessionCore prototypeSessionCore;
        private float gameSpeed;

        public void Awake()
        {
            gameSpeed = prototypeSessionCore.GetGameSpeed();
        }

        public void Update()
        {
            transform.position += new Vector3(0, gameSpeed * Time.deltaTime, 0);
        }

        // перемещение игрока к заданной точке
        public void TimeTransfer(float _startTime, float _gameSpeed)
        {
            transform.position = new Vector3(0, _startTime * _gameSpeed);
        }
    }