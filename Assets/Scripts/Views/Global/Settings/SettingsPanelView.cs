using Controlers;
using Controlers.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Global.Settings
{
    public class SettingsPanelView : MonoBehaviour
    {
        private float Sensetive;
        private float MenuMusicValume;
        private int SessionMusicValume;

        [SerializeField] private Slider SensetiveScrBar;
        [SerializeField] private Slider MenuMusicValumeScrBar;
        [SerializeField] private Slider SessionMusicValumeScrBar;

        [SerializeField] private LevelChooseAudioControler LevelChooseAudioControler;
        
        public void InitView()
        {
            Sensetive = SettingsControler.GetSensetive();
            MenuMusicValume = SettingsControler.GetMenuMusicVolume();
            SessionMusicValume = SettingsControler.GetSessionMusicVolume();

            SensetiveScrBar.value = Sensetive;
            MenuMusicValumeScrBar.value = MenuMusicValume * 100;
            SessionMusicValumeScrBar.value = SessionMusicValume;
        }

        public void SensetiveChanged()
        {
            SettingsControler.SetSensetive(SensetiveScrBar.value);
        }
        
        public void MenuMusicVolumeChanged()
        {
            SettingsControler.SetMenuMusicVolume(MenuMusicValumeScrBar.value);
            LevelChooseAudioControler.SetCurrentValume(SettingsControler.GetMenuMusicVolume());
        }
        
        public void SessionMusicVolumeChanged()
        {
            SettingsControler.SetSessionMusicVolume((int)SessionMusicValumeScrBar.value);
        }
    }
}