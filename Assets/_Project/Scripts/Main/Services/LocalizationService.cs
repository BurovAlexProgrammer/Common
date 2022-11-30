using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Main.Services
{
    public class LocalizationService : MonoBehaviour
    {
        [SerializeField] private Locales _currentLocale;

        public void Init()
        {
            var t = Addressables.LoadAssetAsync<object>("Localization");
        }

        private void LoadLocaleFile()
        {
            
        }
        
        public LocalizedItem GetText(string key)
        {
            
            return null;
        }
    }
}