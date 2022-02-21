using UnityEngine;

    public class PTMovePointComponent : MonoBehaviour
    {
        [SerializeField] private SerciceScreenResolution serciceScreenResolution;
        private float gameSpeed;

        public void Start()
        {
            gameSpeed = serciceScreenResolution.GetScaledGameSpeed();
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