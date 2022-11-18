using System;
using System.Diagnostics;
using System.IO;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

        private static string StoredFolder; //Move to File Service
        private string _storedFilePath;

        public void Init()
        {
            StoredFolder ??= Application.dataPath + "/StoredData/";
            _storedFilePath ??= StoredFolder + $"Stored_{typeof(T).Name}.data";

            // if (_saved == null)
            // {
            //     Debug.LogWarning(
            //         "Current settings reference not found. Default settings using instead without saving.");
            //     _saved = ScriptableObject.CreateInstance<T>();
            //     _saved.CopyDataFrom(_default);
            // }
            // _current = ScriptableObject.CreateInstance<T>();
            // _current.CopyDataFrom(_saved);
            
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
                var storedData = JsonConvert.DeserializeObject<T>(json, new SOConverter<T>());
                _saved.CopyDataFrom(storedData);
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
        
        public class SOConverter<T> : CustomCreationConverter<T> where T : ScriptableObject
        {
            public override T Create(Type aObjectType)
            {
                if (typeof(T).IsAssignableFrom(aObjectType))
                    return (T)ScriptableObject.CreateInstance(aObjectType);
                return null;
            }
        }
    }
}