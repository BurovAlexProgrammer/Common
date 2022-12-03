using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Extension.Editor
{
    public class LocalizationToolsWindow : EditorWindow
    {
        private int _selectedLocale = 0;
        private LocalizationToolsSettings _settings;
        
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
            _settings = Common.LoadSingleAsset<LocalizationToolsSettings>();
            var localeStorePath = _settings.LocalizationStorePath;
            
            
        }

        private void OnGUI()
        {
            _selectedLocale = GUILayout.Toolbar (_selectedLocale, new string[] {"Object", "Bake", "Layers"});
            switch (_selectedLocale) {
            }
            Debug.Log(_selectedLocale);
            
            if (GUILayout.Button("Init")) Init();
        }
    }
}