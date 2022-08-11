using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [Serializable]
    public class SettingsManager
    {
        [SerializeField] private SettingGroup<VideoSettings> _videoSettings;

        private List<SettingGroup> settingsList;
        private Dictionary<Type, SettingGroup> _settingDictionary;

        public void Init()
        {
            _settingDictionary = new Dictionary<Type, SettingGroup>
            {
                { _videoSettings.SettingsType, _videoSettings} //TODO optimize
            };
            
            settingsList = new List<SettingGroup>
            {
                _videoSettings
            };

            foreach (var settingGroup in settingsList)
            {
                var t = settingGroup.GetType();
            }

            LoadOrDefaultSetting();
            
        }

        public void Add()
        {
            _videoSettings.Current.number++;
        }

        private void LoadOrDefaultSetting()
        {
        }


        public void Cancel()
        {
            
        }
    }
}