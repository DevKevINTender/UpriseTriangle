using UnityEditor;
using UnityEngine;

namespace Controlers
{
    public class SegmentControler
    {
        private static int StorageSegments = PlayerPrefs.GetInt("UserSegmentCount", 0);
        private static int totalSegmentCost = 500;
        public static void UpcreaseSegment(int count)
        {
            StorageSegments += count; 
            PlayerPrefs.SetInt("UserSegmentCount", StorageSegments);
        }
        
        public static void DecreaseSegment(int count)
        {
            StorageSegments -= count; 
            PlayerPrefs.SetInt("UserSegmentCount", StorageSegments);
            
        }

        public static int GetSegmentCount()
        {
            return StorageSegments;
        }
        public static bool BuySegment()
        {
            if (CoinsControler.BuySegment(totalSegmentCost))
            {
                UpcreaseSegment(1);
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}