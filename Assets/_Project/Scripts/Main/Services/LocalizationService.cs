using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Task = System.Threading.Tasks.Task;

namespace _Project.Scripts.Main.Services
{
    public class LocalizationService : MonoBehaviour
    {
        [SerializeField] private Locales _currentLocale;
        private Dictionary<Locales, Localization> _localizations;

        public async void Init()
        {
            var aoHandles = new List<AsyncOperationHandle<TextAsset>>();
            var resourceLocations = Addressables.LoadResourceLocationsAsync("locales").WaitForCompletion();
            
            foreach (var resourceLocation in resourceLocations)
            {
                aoHandles.Add(Addressables.LoadAssetAsync<TextAsset>(resourceLocation));
            }

            await Task.WhenAll(aoHandles.Select(x => x.Task).ToArray());
            foreach (var aoHandle in aoHandles)
            {
                LoadLocaleFile(aoHandle.Result);
                
            }
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
                Debug.Log(lines[i]);
            }

            return new Localization(locale, formatInfoMaybeJson, lines);
        }
        
        public LocalizedItem GetText(string key)
        {
            
            return null;
        }
    }
}