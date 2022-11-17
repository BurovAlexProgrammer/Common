using System;
using _Project.Scripts.Extension;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    public interface ISettingGroup
    {
        void Init();
        void Load();
        void Save();
        void Cancel();
        void Restore();
    }

    [Serializable]
    public class SettingGroup<T> : ISettingGroup where T : SettingsSO
    {
        [SerializeField] private T _default;
        [SerializeField] private T _saved;
        [SerializeField] private T _current;
        
        public T CurrentSettings => _current;

        public void Init()
        {
            if (_saved == null)
            {
                Debug.LogWarning(
                    "Current settings reference not found. Default settings using instead without saving.");
                _saved = ScriptableObject.CreateInstance<T>();
                _saved.CopyDataFrom(_default);
            }

            _current = ScriptableObject.CreateInstance<T>();
            _current.CopyDataFrom(_saved);
        }

        public void Load()
        {
            _current.CopyDataFrom(_saved);
        }

        public void Save()
        {
            _saved.CopyDataFrom(_current);
        }

        public void Cancel()
        {
            _current.CopyDataFrom(_saved);
        }

        public void Restore()
        {
            _current.CopyDataFrom(_default);
            _saved.CopyDataFrom(_default);
        }
    }
}