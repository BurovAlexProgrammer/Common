using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Extension.Editor.LocalizationTools
{
    public class LocalizationToolsWindow : EditorWindow
    {
        private int _selectedLocaleIndex;
        private Localization _selectedLocalizationInstance;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        private string[] _localeNames;
        
        private Vector2 _tableScrollPos;
        private Rect _tableScrollViewRect;
        private bool _selectedOriginal;

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
            _selectedOriginal = true;
            MarkOriginalTab();
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
            GUILayout.Space(8);
            DrawLocalizationTable();
        }

        private void DrawLocalizationTable()
        {

            GUILayout.BeginHorizontal();
                GUILayout.Label("#",GUILayout.Width(32));
                GUILayout.Label("Key",GUILayout.Width(200));
                GUILayout.Label("Description",GUILayout.Width(200));
                GUILayout.Label("Original",GUILayout.Width(300));
                if (!_selectedOriginal)
                {
                    GUILayout.Label("Text");
                }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Separator();
            
            _tableScrollPos = GUILayout.BeginScrollView(_tableScrollPos, false, true);
            
                var number = 0;
                foreach (var (key, localizedItem) in _selectedLocalizationInstance.LocalizedItems)
                {
                    GUILayout.BeginHorizontal();
                        GUILayout.Label((++number).ToString(), GUILayout.Width(32));
                        EditorGUIUtility.labelWidth = 32;
                        localizedItem.Key = GUILayout.TextField(localizedItem.Key, GUILayout.Width(200));
                        EditorGUIUtility.labelWidth = 64;
                        localizedItem.Description = GUILayout.TextField(localizedItem.Description, GUILayout.Width(200));
                        EditorGUIUtility.labelWidth = 50;
                        localizedItem.Original = GUILayout.TextField(localizedItem.Original,  _selectedOriginal ? GUILayout.ExpandWidth(true) : GUILayout.Width(300));

                        if (!_selectedOriginal)
                        {
                            EditorGUIUtility.labelWidth = 60;
                            localizedItem.Text = GUILayout.TextField(localizedItem.Text);
                        }
                        
                    GUILayout.EndHorizontal();
                }

            GUILayout.EndScrollView();
        }

        private void MarkOriginalTab()
        {
            for (var i = 0; i < _localeNames.Length; i++)
            {
                if (_localeNames[i] == _originalLocalization.Info.name) _localeNames[i] += " *";
            }
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
            _selectedOriginal = selectedLocalization.Info.name == _originalLocalization.Info.name;
        }

        private GUILayoutOption[] textFieldOptions = {
            GUILayout.ExpandWidth(false),
        };
    }
}