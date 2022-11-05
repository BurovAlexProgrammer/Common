using System;
using System.Collections.Generic;
using UnityEngine;


namespace _Project.Scripts.Settings
{
    public class SettingsService : MonoBehaviour
    {
        [SerializeField] private SettingGroup<VideoSettings> _videoSettings;
        [SerializeField] private SettingGroup<AudioSettings> _audioSettings;

        public VideoSettings Video => _videoSettings.CurrentSettings;
        public AudioSettings Audio => _audioSettings.CurrentSettings;

        private List<SettingGroup> _settingList;

        public void Init()
        {
            //TODO don't forget to add all above SettingsGroups to list 
            _settingList = new List<SettingGroup>
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