using UnityEditor;
using UnityEngine;

namespace Controlers
{
    public class SegmentControler
    {
        public delegate void AccountHandler(int segmentCount);
        public static event AccountHandler DecreaseSegmentsEvent;
        public static event AccountHandler IncreaseSegmentsEvent;
        
        private static int StorageSegments = PlayerPrefs.GetInt("UserSegmentCount", 0);
        private static int totalSegmentCost = 500;
        public static void UpcreaseSegment(int count)
        {
            StorageSegments += count; 
            PlayerPrefs.SetInt("UserSegmentCount", StorageSegments);
            IncreaseSegmentsEvent?.Invoke(count);
        }
        
        public static void DecreaseSegment(int count)
        {
            StorageSegments -= count; 
            PlayerPrefs.SetInt("UserSegmentCount", StorageSegments);
            DecreaseSegmentsEvent?.Invoke(count);
        }

        public static int GetSegmentCost()
        {
            return totalSegmentCost;
        }
        public static int GetSegmentCount()
        {
            return StorageSegments;
        }
        public static bool BuySegment(int count)
        {
            if (CoinsControler.BuySegment(totalSegmentCost * count))
            {
                UpcreaseSegment(count);
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}