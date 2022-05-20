using UnityEditor;
using UnityEngine;

namespace Controlers
{
    public class CoinsControler
    {
        private static int StorageCoins = PlayerPrefs.GetInt("UserCoinsCount", 0);
        
        public static void UpcreaseCoins(int count)
        {
            StorageCoins += count; 
            PlayerPrefs.SetInt("UserCoinsCount", StorageCoins);
        }
        
        public static void DecreaseCoins(int count)
        {
            StorageCoins -= count; 
            PlayerPrefs.SetInt("UserCoinsCount", StorageCoins);
            
        }

        public static int GetCoinsCount()
        {
            return StorageCoins;
        }
        public static bool BuyEffect(int cost)
        {
            if (StorageCoins >= cost)
            {
                Debug.Log("Cost " + cost);
                DecreaseCoins(cost);
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static void BuySessionLevel(int cost)
        {
            Debug.Log("Cost " + cost);
            DecreaseCoins(cost);
        }

        public static bool BuySegment(int cost)
        {
            if (StorageCoins >= cost)
            {
                DecreaseCoins(cost);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}