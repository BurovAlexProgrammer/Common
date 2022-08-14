using UnityEngine;

namespace _Project.Scripts.Settings
{
    namespace _Project.Scripts.Settings
    {
        [CreateAssetMenu(menuName = "Custom/Settings/Audio")]
        public class AudioSettings : SettingsSO
        {
            public bool SoundEnabled;
            public float SoundVolume;
            public bool MusicEnabled;
            public float MusicVolume;
        }
        
        public static class AudioSettingsAttributes
        {
            public static float VolumeMin = 0f;
            public static float VolumeMax = 20f;
        }
    }
}