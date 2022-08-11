using System;
using UnityEngine;

namespace _Project.Scripts.Settings
{
    public abstract class SettingsSOBase : ScriptableObject
    {
        public string Description;
    }
    
    public class SettingsSO : SettingsSOBase
    {
    }
}