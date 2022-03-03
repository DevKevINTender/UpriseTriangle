using UnityEngine;
using UnityEngine.UI;

    public class PTMovePointComponent : MonoBehaviour
    {
        [SerializeField] private SerciceScreenResolution serciceScreenResolution;
        [SerializeField] private Text speed;
        private float gameSpeed;

        public void Start()
        {
            gameSpeed = serciceScreenResolution.GetScaledGameSpeed();
        }

        public void Update()
        {
            transform.position += new Vector3(0, gameSpeed * Time.deltaTime, 0);
            speed.text = (gameSpeed * Time.deltaTime).ToString("F3");
        }

        // перемещение игрока к заданной точке
        public void TimeTransfer(float _startTime, float _gameSpeed)
        {
            transform.position = new Vector3(0, _startTime * _gameSpeed);
        }
    }