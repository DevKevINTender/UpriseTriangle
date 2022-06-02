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