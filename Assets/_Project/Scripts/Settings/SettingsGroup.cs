using System;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    [Serializable]
    public class SettingsGroup<T> where T:Settings
    {
        [SerializeField] private T _default;

        public T Default => _default;
        public T Current => _current;
        public T Loaded => _loaded;
        
        private T _current;
        private T _loaded;

        public SettingsGroup(T defaultSettings)
        {
            _default = defaultSettings;
            _loaded = defaultSettings;
            _current = defaultSettings;
        }
    }
}