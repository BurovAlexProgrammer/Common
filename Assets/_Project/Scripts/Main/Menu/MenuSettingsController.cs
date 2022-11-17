using UnityEngine;
using Zenject;
using SettingsService = _Project.Scripts.Main.Services.SettingsService;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsController : MonoBehaviour
    {
        private SettingsService _settings;
        
        [Inject]
        public void Construct(SettingsService settingsService)
        {
            _settings = settingsService;
        }

        public void Apply()
        {
            _settings.Save();

        }

        public void ResetToDefault()
        {
            _settings.Restore();
        }

        public void Add()
        {
            _settings.Video.number++;
        }
    }
}