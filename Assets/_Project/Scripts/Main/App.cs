using System;
using _Project.Scripts.Settings;
using UnityEngine;

namespace _Project.Scripts
{
    public class App : MonoBehaviour
    {
        // public static App Instance;
        // public static SettingsManager Settings => Instance.SettingsManager;
        //
        // [SerializeField] private SettingsManager _settingsManager;
        //
        // public SettingsManager SettingsManager => _settingsManager;
        //
        // private void Awake()
        // {
        //     Init();
        // }
        //
        // private void Init()
        // {
        //     if (Instance == null)
        //     {
        //         Instance = this;
        //         DontDestroyOnLoad(this);
        //         _settingsManager.Init();
        //     }
        //     else
        //     {
        //         throw new Exception();
        //     }
        // }

        // #if UNITY_EDITOR
        // [UnityEditor.Callbacks.DidReloadScripts]
        // private static void OnScriptsReloaded()
        // {
        //     //TODO find DoNotDestroyed App
        // }
        // #endif
    }
}