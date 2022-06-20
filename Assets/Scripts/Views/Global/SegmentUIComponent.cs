using System.Collections;
using Controlers;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Global
{
    public class SegmentUIComponent : MonoBehaviour
    {
        [SerializeField] private GameObject segmentsManipulatePB;
         private GameObject segmentsManipulateObj;
         private Text segmentsManipulateText;
         [Header("Colors")]
         [SerializeField] private Color32 increaseColor;
         [SerializeField] private Color32 decreaseColor;
     
         private Coroutine Increasecoroutine;
         private Coroutine Decreasecoroutine;
         private bool increaseDelayWorking;
         private bool decreaseDelayWorking;
         private int totalSegmentsCount;
     
         void Start()
         {
             increaseDelayWorking = false;
             transform.GetComponent<Text>().text = $"{SegmentControler.GetSegmentCount()}";
             SegmentControler.DecreaseSegmentsEvent += SegmentDecreaseView;
             SegmentControler.IncreaseSegmentsEvent += SegmentIncreaseView;
         }

         public void UpdateCoinView()
         {
            transform.GetComponent<Text>().text = $"{SegmentControler.GetSegmentCount()}";
         }

        public void SegmentIncreaseView(int segmentsCount)
         {
            if (transform.gameObject.activeInHierarchy)
            {
                if (increaseDelayWorking)
                {
                    StopCoroutine(Increasecoroutine);
                }
                Increasecoroutine = StartCoroutine(IncreaseDelay(segmentsCount));
            }
         }

        public void SegmentDecreaseView(int coinsCount)
        {
            if (transform.gameObject.activeInHierarchy)
            {
                if (decreaseDelayWorking)
                {
                    StopCoroutine(Decreasecoroutine);
                }
                Decreasecoroutine = StartCoroutine(DecreaseDelay(coinsCount));
            }
        }

        private IEnumerator DecreaseDelay(int coinsCount)
        {
            decreaseDelayWorking = true;
            totalSegmentsCount += coinsCount;
            yield return new WaitForSeconds(0.3f);
            segmentsManipulateText = SpawnSegmentsChangeView();
            segmentsManipulateText.color = decreaseColor;
            segmentsManipulateText.text = "-" + totalSegmentsCount;
            UpdateCoinView();
            decreaseDelayWorking = false;
            totalSegmentsCount = 0;
        }

        private IEnumerator IncreaseDelay(int segmentsCount)
         {
             increaseDelayWorking = true;
             totalSegmentsCount += segmentsCount;
             yield return new WaitForSeconds(0.5f);
             segmentsManipulateText = SpawnSegmentsChangeView();
             segmentsManipulateText.color = increaseColor;
             segmentsManipulateText.text = "+" + totalSegmentsCount;
            UpdateCoinView();
             increaseDelayWorking = false;
             totalSegmentsCount = 0;
         }
     
         public Text SpawnSegmentsChangeView()
         {
             segmentsManipulateObj = Instantiate(segmentsManipulatePB, transform);
             segmentsManipulateObj.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
             return segmentsManipulateObj.GetComponent<Text>();
         }
        
    }
}