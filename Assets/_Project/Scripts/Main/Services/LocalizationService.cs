using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Main.Services
{
    public class LocalizationService : MonoBehaviour
    {
        [SerializeField] private Locales _currentLocale;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _currentLocalization;
        private bool _isLoaded;

        public bool IsLoaded => _isLoaded;
        
        public async void Init()
        {
            _localizations = new Dictionary<Locales, Localization>();
            var aoHandles = new List<AsyncOperationHandle<TextAsset>>();
            var resourceLocations = Addressables.LoadResourceLocationsAsync("locales").WaitForCompletion();
            
            foreach (var resourceLocation in resourceLocations)
            {
                aoHandles.Add(Addressables.LoadAssetAsync<TextAsset>(resourceLocation));
            }

            await Task.WhenAll(aoHandles.Select(x => x.Task).ToArray());
            var localeTextList = aoHandles.Select(x => x.Result).ToList();
            
            foreach (var localeText in localeTextList)
            {
                var localization = LoadLocaleFile(localeText);
                _localizations.Add(localization.Locale, localization);
            }

            if (!_localizations.ContainsKey(_currentLocale))
                throw new Exception("Current localization not found.");

            _currentLocalization = _localizations[_currentLocale];
            _isLoaded = true;
        }

        private Localization LoadLocaleFile(TextAsset textAsset)
        {
            var lines = textAsset.text.SplitLines();
            var locale = lines[0];
            var formatInfoMaybeJson = lines[1];
            var hint = lines[2];

            var itemList = new List<string>();
            for (var i = 3; i < lines.Length; i++)
            {
                itemList.Add(lines[i]);
            }

            return new Localization(locale, formatInfoMaybeJson, itemList.ToArray());
        }
        
        public string GetLocalizedText(string key)
        {
            return _currentLocalization.LocalizedItems[key].Text;
        }
    }
}