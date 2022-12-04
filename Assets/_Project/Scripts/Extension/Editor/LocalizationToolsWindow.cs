using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Main.Wrappers;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Extension.Editor
{
    public class LocalizationToolsWindow : EditorWindow
    {
        private int _selectedLocaleIndex;
        private Localization _selectedLocalizationInstance;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        private string[] _localeNames;

        [MenuItem ("Tools/Localization Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(LocalizationToolsWindow)) as LocalizationToolsWindow;
            window.titleContent = new GUIContent { text = "Localization Editor" };
            window.Focus();
            window.Init();
            window.Show();
        }

        private void Init()
        {
            Debug.Log("Init");
            _localizations = LocalizationTools.Instance.Localizations;
            _originalLocalization = LocalizationTools.Instance.OriginalLocalization;
            _localeNames = _localizations.Values.Select(x => x.Info.name).ToArray();
            _selectedLocalizationInstance = new Localization(_originalLocalization);
        }

        private void OnGUI()
        {
            if (_localeNames.Length == 0) Init();

            GUILayout.ExpandWidth(false);
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Localizations: ", GUILayout.ExpandWidth(false));
            var selectedLocaleIndex = GUILayout.Toolbar (_selectedLocaleIndex, _localeNames, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(128));
            GUILayout.EndHorizontal();

            if (selectedLocaleIndex != _selectedLocaleIndex)
            {
                SwitchLocale(selectedLocaleIndex);
            }
            
            if (GUILayout.Button("Init")) Init();
            
            GUILayout.Space(8);
            DrawLocalizationInfo();
        }

        private void DrawLocalizationInfo()
        {
            var info = _selectedLocalizationInstance.Info;
            GUI.changed = false;
            GUILayout.Label("Info: ", GUILayout.ExpandWidth(false));
            GUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 40;
                info.name = EditorGUILayout.TextField("Name", info.name, textFieldOptions);
                GUILayout.Space(16);
                EditorGUIUtility.labelWidth = 60;
                info.fullName = EditorGUILayout.TextField("FullName", info.fullName, textFieldOptions);
            GUILayout.EndHorizontal();
            
            if (GUI.changed) { }
        }

        private void SwitchLocale(int newIndex)
        {
            Debug.Log("SwitchLocale");
            _selectedLocaleIndex = newIndex;
            var selectedLocalization = _localizations.Where((x, i) => i == newIndex).Single().Value;
            _selectedLocalizationInstance = new Localization(selectedLocalization);
        }

        private GUILayoutOption[] textFieldOptions = {
            GUILayout.ExpandWidth(false),
        };
    }
}