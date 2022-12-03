using _Project.Scripts.Main.Wrappers;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace _Project.Scripts.Extension.Editor
{
    [CreateAssetMenu(menuName = "Custom/Localization/Settings")]
    public class LocalizationToolsSettings : ScriptableObject
    {
        [SerializeField] private DefaultAsset _localeStoreFolder;
        [SerializeField] private Locales _originalLocales;

        public Locales OriginalLocales => _originalLocales;

        public string LocalizationStorePath => _localeStoreFolder == null ? null : AssetDatabase.GetAssetPath(_localeStoreFolder);
    }
}