using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(menuName = "Custom/Settings/Video")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing;
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion;
        public bool PostProcessDepthOfField;
    }

    public static class VideoSettingsAttributes
    {
        
    }
}