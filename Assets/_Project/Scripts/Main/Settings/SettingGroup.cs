using System;
using System.IO;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Main.Wrappers;
using _Project.Scripts.Settings;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using static _Project.Scripts.Extension.Common;
using Debug = UnityEngine.Debug;

namespace _Project.Scripts.Main.Settings
{
    public interface ISettingGroup
    {
        void Init();
        void Load();
        void Save();
        void Cancel();
        void Restore();
        void ApplySettings();
    }

    [Serializable]
    public class SettingGroup<T> : ISettingGroup where T : SettingsSO
    {
        [SerializeField] private T _default;
        [SerializeField] private T _saved;
        [SerializeField] private T _current;
        
        public T CurrentSettings => _current;

        private static string StoredFolder; //TODO Move to File Service
        private string _storedFilePath;

        public void Init()
        {
            Load();
            _saved.Init();
            ApplySettings();
        }

        public void ApplySettings()
        {
            _saved.ApplySettings();
        }

        public void Load()
        {
            StoredFolder ??= Application.dataPath + "/StoredData/";
            _storedFilePath ??= StoredFolder + $"Stored_{typeof(T).Name}.data";

            Directory.CreateDirectory(StoredFolder);
            _saved = ScriptableObject.CreateInstance<T>();

            if (!File.Exists(_storedFilePath))
            {
                CreateDefaultSettingFile();
                Debug.LogWarning($"Stored file '{_storedFilePath}' not found. Default settings using instead.");
                _saved.CopyDataFrom(_default);
            }
            else
            {
                var json = File.ReadAllText(_storedFilePath);
                var storedData = JsonConvert.DeserializeObject<T>(json, new ScriptableObjectConverter<T>());
                _saved.CopyDataFrom(storedData);
            }

            _current = ScriptableObject.CreateInstance<T>();
            _current.CopyDataFrom(_saved);
        }

        public void Save()
        {
            var data = Serializer.ToJson(_current);
            File.WriteAllText(_storedFilePath, data);
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

        private void CreateDefaultSettingFile()
        {
            var data = Serializer.ToJson(_default);
            File.WriteAllText(_storedFilePath, data);
        }
    }
}