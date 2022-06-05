using Controlers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using  static SegmentStoreView;
namespace Views.Global
{
    public class SegmentBuyPanelItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int segmentCount;
        [Header("Visual")]
        [SerializeField] private Text segmentCountText;
        [SerializeField] private Text segmentCostText;

        private segmentBuyDelegate buySegment;
        
        public void InitView(segmentBuyDelegate buySegment)
        {
            this.buySegment = buySegment;
            segmentCountText.text = "x" + segmentCount;
            segmentCostText.text = "" + SegmentControler.GetSegmentCost() * segmentCount;

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            buySegment?.Invoke(segmentCount);
        }
    }
    
}