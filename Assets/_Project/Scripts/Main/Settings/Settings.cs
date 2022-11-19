using UnityEngine;

namespace _Project.Scripts.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings();
    }
}