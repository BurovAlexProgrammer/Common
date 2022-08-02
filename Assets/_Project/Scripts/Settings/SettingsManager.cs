using System;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [Serializable]
    public class SettingsManager
    {
        [SerializeField] private SettingsGroup<VideoSettings> _videoSettings; 
        
        public SettingsGroup<VideoSettings> Video => _videoSettings;
    }
}