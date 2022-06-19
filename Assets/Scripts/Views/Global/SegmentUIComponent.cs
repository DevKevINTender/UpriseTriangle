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
         private bool increaseDelayWorking;
         private int totalSegmentsCount;
     
         void Start()
         {
             increaseDelayWorking = false;
             transform.GetComponent<Text>().text = $"{SegmentControler.GetSegmentCount()}";
             SegmentControler.DecreaseSegmentsEvent += SegmentDecreaseView;
             SegmentControler.IncreaseSegmentsEvent += SegmentIncreaseView;
         }
     
         public void SegmentIncreaseView(int segmentsCount)
         {
             if (increaseDelayWorking)
             {
                 StopCoroutine(Increasecoroutine);
             }
             Increasecoroutine = StartCoroutine(IncreaseDelay(segmentsCount));
         }
     
         public void SegmentDecreaseView(int segmentsCount)
         {
             segmentsManipulateText = SpawnSegmentsChangeView();
             segmentsManipulateText.color = decreaseColor;
             segmentsManipulateText.text = "-" + segmentsCount;
         }
     
         private IEnumerator IncreaseDelay(int segmentsCount)
         {
             increaseDelayWorking = true;
             totalSegmentsCount += segmentsCount;
             yield return new WaitForSeconds(0.3f);
             segmentsManipulateText = SpawnSegmentsChangeView();
             segmentsManipulateText.color = increaseColor;
             segmentsManipulateText.text = "+" + totalSegmentsCount;
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