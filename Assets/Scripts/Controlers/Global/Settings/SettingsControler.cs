using UnityEngine;

namespace Controlers.Settings
{
    public static class SettingsControler
    {
        private static int Sensetive;
        private static float MenuMusicVolume;
        private static int SessionMusicVolume;

        public static int GetSensetive()
        {
            Sensetive = PlayerPrefs.GetInt("Sensetive", 0);
            return Sensetive;
        }

        public static float GetMenuMusicVolume()
        {
            MenuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0f) ;
            return MenuMusicVolume;
        }

        public static int GetSessionMusicVolume()
        {
            SessionMusicVolume = PlayerPrefs.GetInt("SessionMusicVolume", 0);
            return SessionMusicVolume;
        }
        public static void SetSensetive(int newSensetive)
        {
            Sensetive = newSensetive;
            PlayerPrefs.SetInt("Sensetive", newSensetive);
        }

        public static void SetMenuMusicVolume(float newMenuMusicVolume)
        {
            MenuMusicVolume = newMenuMusicVolume;
            PlayerPrefs.SetFloat("MenuMusicVolume", newMenuMusicVolume / 100);
        }

        public static void SetSessionMusicVolume(int newSessionMusicVolume)
        {
            SessionMusicVolume = newSessionMusicVolume;
            PlayerPrefs.SetInt("SessionMusicVolume", newSessionMusicVolume);
        }
    }
}