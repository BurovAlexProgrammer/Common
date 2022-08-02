using System;
using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts
{
    public class App : MonoBehaviour
    {
        public static App Instance;
        public static SettingsManager Settings => Instance.SettingsManager;

        [SerializeField] private SettingsManager _settingsManager;

        public SettingsManager SettingsManager => _settingsManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}