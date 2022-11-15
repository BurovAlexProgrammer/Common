using UnityEngine;
using Zenject;
using SettingsService = _Project.Scripts.Settings.SettingsService;

namespace _Project.Scripts.Main
{
    public class MenuSettingsController : MonoBehaviour
    {
        private SettingsService _settings;
        
        [Inject]
        public void Construct(SettingsService settingsService)
        {
            _settings = settingsService;
            Debug.LogWarning(_settings);
        }

        public void Apply()
        {
            _settings.Save();
            Debug.LogWarning(_settings);

        }

        public void ResetToDefault()
        {
            _settings.Restore();
            Debug.LogWarning(_settings);

        }

        public void Add()
        {
            _settings.Video.number ++;
            Debug.LogWarning(_settings);

        }
    }
}