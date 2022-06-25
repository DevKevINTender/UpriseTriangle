using UnityEngine;

namespace Controlers.Settings
{
    public static class SettingsControler
    {
        private static float Sensetive;
        private static float MenuMusicVolume;
        private static int SessionMusicVolume;

        public static float GetSensetive()
        {
            Sensetive = PlayerPrefs.GetFloat("Sensetive", 1);
            return Sensetive;
        }

        public static float GetMenuMusicVolume()
        {
            MenuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 1f) ;
            return MenuMusicVolume;
        }

        public static int GetSessionMusicVolume()
        {
            SessionMusicVolume = PlayerPrefs.GetInt("SessionMusicVolume", 10);
            return SessionMusicVolume;
        }
        public static void SetSensetive(float newSensetive)
        {
            Sensetive = newSensetive;
            PlayerPrefs.SetFloat("Sensetive", newSensetive);
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