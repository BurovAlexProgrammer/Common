using System;
using System.ComponentModel.Design.Serialization;
using Global;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Settings
{
    public interface SettingGroup
    {
    }
    
    [Serializable]
    public class SettingGroup<T> : SettingGroup where T : ScriptableObject
    {
        public T Default => _default;
        public T Current => _current;
        public Type SettingsType => typeof(T);


        [SerializeField] private T _default;
        [SerializeField] private T _current;
        private T _lastSaved;

        public void Init()
        {
            if (_current == null)
            {
                Debug.LogWarning("Current settings reference not found. Default settings using instead without saving.");
                _current = ScriptableObject.CreateInstance<T>();
                _default.CopyDataTo(_current);
            }

            _lastSaved = ScriptableObject.CreateInstance<T>();
            _current.CopyDataTo(_lastSaved);
        }

    }
}