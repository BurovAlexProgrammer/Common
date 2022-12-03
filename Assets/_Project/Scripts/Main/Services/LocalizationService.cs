using System;
using System.Collections.Generic;
using System.IO;
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

        private const Locales OriginalLocale = Locales.en_EN;

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

            for (var i = 0; i < aoHandles.Count; i++)
            {
                var localeText = aoHandles[i].Result;
                var filePath = resourceLocations[i];
                var localization = LoadLocaleFile(localeText, filePath.ToString());
                _localizations.Add(localization.Locale, localization);
            }

            if (!_localizations.ContainsKey(_currentLocale))
                throw new Exception("Current localization not found.");

            _currentLocalization = _localizations[_currentLocale];
            _isLoaded = true;
        }

        private Localization LoadLocaleFile(TextAsset textAsset, string filePath)
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

            return new Localization(locale, formatInfoMaybeJson, itemList.ToArray(), filePath);
        }
        
        public string GetLocalizedText(string key)
        {
            if (!_currentLocalization.LocalizedItems.ContainsKey(key))
            {
                AddNewKeyToDictionary(key);
                return _currentLocalization.LocalizedItems[key].Key;
            }
            
            return _currentLocalization.LocalizedItems[key].Text;
        }

        private void AddNewKeyToDictionary(string newKey)
        {
            //TODO record to files
            if (Application.isEditor)
            {
                Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'.");
                var localizations = _localizations.Select((x) => x.Value);
                foreach (var localization in localizations)
                {
                    if (localization.LocalizedItems.ContainsKey(newKey) == false)
                    {
                        Debug.LogWarning($"The key is not in locale '{localization.Locale.ToString()}'. Adding new key..");
                        var fullPath = Application.dataPath + localization.FilePathInEditor.Replace("Assets", "");
                        using var streamWriter = File.AppendText(fullPath);
                        streamWriter.WriteLine(newKey + ";;;;");
                        localization.LocalizedItems.Add(newKey, new LocalizedItem() {Key = newKey, Text = "NOT TRANSLATED"});
                    }
                }
            }
            else
            {
                Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'");
            }
        }
    }
}