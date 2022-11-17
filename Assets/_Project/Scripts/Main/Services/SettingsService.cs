using System.Collections.Generic;
using _Project.Scripts.Settings;
using UnityEngine;
using AudioSettings = _Project.Scripts.Settings.AudioSettings;

namespace _Project.Scripts.Main.Services
{
    public class SettingsService : MonoBehaviour
    {
        [SerializeField] private SettingGroup<VideoSettings> _videoSettings;
        [SerializeField] private SettingGroup<AudioSettings> _audioSettings;

        public VideoSettings Video => _videoSettings.CurrentSettings;
        public AudioSettings Audio => _audioSettings.CurrentSettings;

        private List<ISettingGroup> _settingList;

        public void Init()
        {
            _settingList = new List<ISettingGroup>
            {
                _audioSettings, 
                _videoSettings
            };

            foreach (var settingGroup in _settingList)
            {
                settingGroup.Init();
            }
        }

        public void Load()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Load();
            }
        }

        public void Save()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Save();
            }
        }

        public void Cancel()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Cancel();
            }
        }

        public void Restore()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Restore();
            }
        }
    }
}