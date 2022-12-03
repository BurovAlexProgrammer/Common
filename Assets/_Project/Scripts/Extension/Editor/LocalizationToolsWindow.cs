using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.Main.Wrappers;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Extension.Editor
{
    public class LocalizationToolsWindow : EditorWindow
    {
        private int _selectedLocaleIndex = 0;
        private Localization _selectedLocalization;
        private LocalizationToolsSettings _settings;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        private string[] _localeNames;

        [MenuItem ("Tools/Localization Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(LocalizationToolsWindow)) as LocalizationToolsWindow;
            window.titleContent = new GUIContent() { text = "Localization Editor" };
            window.Focus();
            window.Init();
            window.Show();
        }

        private void Init()
        {
            _localizations = LocalizationTools.Instance.Localizations;
            _localeNames = _localizations.Values.Select(x => x.Info.name).ToArray();
            _originalLocalization = _localizations.Single(x => x.Key == _settings.OriginalLocales).Value;
            _selectedLocalization = _originalLocalization;
        }

        private void OnGUI()
        {
            if (_localeNames.Length == 0) Init();
            
            var selectedLocaleIndex = GUILayout.Toolbar (_selectedLocaleIndex, _localeNames);

            if (selectedLocaleIndex != _selectedLocaleIndex)
            {
                SwitchLocale(selectedLocaleIndex);
            }
            
            if (GUILayout.Button("Init")) Init();
        }

        private void SwitchLocale(int newIndex)
        {
            Debug.Log("SwitchLocale");
            _selectedLocaleIndex = newIndex;
            _selectedLocalization = _localizations.Where((x,i) => i == newIndex).Single().Value;
        }
    }
}