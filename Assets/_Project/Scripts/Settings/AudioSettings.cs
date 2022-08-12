using UnityEngine;

namespace _Project.Scripts.Settings
{
    namespace _Project.Scripts.Settings
    {
        [CreateAssetMenu(menuName = "Custom/Settings/Audio")]
        public class AudioSettings : SettingsSO
        {
            public float VolumeMin;
            public float VolumeMax;
            public bool MusicEnabled;
            public bool SoundEnabled;
            public float MusicVolume;
        }
    }
}