using UnityEditor;
using UnityEngine;
using System;

namespace Controlers
{
    public class CoinsControler
    {
        public delegate void AccountHandler(int coinCount);
        public static event AccountHandler DecreaseCoinsEvent;
        public static event AccountHandler IncreaseCoinsEvent;

        private static int StorageCoins = PlayerPrefs.GetInt("UserCoinsCount", 0);
        
        public static void IncreaseCoins(int count)
        {
            StorageCoins += count; 
            PlayerPrefs.SetInt("UserCoinsCount", StorageCoins);
            IncreaseCoinsEvent?.Invoke(count);
        }
        
        public static void DecreaseCoins(int count)
        {
            StorageCoins -= count; 
            PlayerPrefs.SetInt("UserCoinsCount", StorageCoins);
            DecreaseCoinsEvent?.Invoke(count);
        }

        public static int GetCoinsCount()
        {
            return StorageCoins;
        }


        public static bool BuyEffect(int cost)
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
        public static void BuySessionLevel(int cost)
        {
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