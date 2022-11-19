using System;
using _Project.Scripts.Main.Services;
using _Project.Scripts.Settings;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;
using static UnityEngine.Rendering.PostProcessing.PostProcessLayer.Antialiasing;

namespace _Project.Scripts.Main.Settings
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Settings/Video")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing;
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion;
        public bool PostProcessDepthOfField;
        public bool PostProcessChromaticAberration;
        public bool PostProcessLensDistortion;
        public bool PostProcessMotionBlur;
        

        private ScreenService _screenService;
        private PostProcessLayer _postProcessLayer;
        private PostProcessVolume _postProcessVolume;

        [Inject]
        public void Construct(ScreenService screenService)
        {
            _screenService = screenService;
            _postProcessLayer = screenService.PostProcessLayer;
            _postProcessVolume = screenService.PostProcessVolume;
        }

        public override void ApplySettings()
        {
            _postProcessLayer.antialiasingMode = PostProcessAntiAliasing ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : None;
            foreach (var effectSettings in _postProcessVolume.sharedProfile.settings)
            {
                switch (effectSettings)
                {
                    case AmbientOcclusion ambientOcclusion:
                        effectSettings.enabled.value = PostProcessAmbientOcclusion;
                        break;
                    case Bloom bloom:
                        effectSettings.enabled.value = PostProcessBloom;
                        break;
                    case ChromaticAberration chromaticAberration:
                        effectSettings.enabled.value = PostProcessChromaticAberration;
                        break;
                    case DepthOfField depthOfField:
                        effectSettings.enabled.value = PostProcessDepthOfField;
                        break;
                    case LensDistortion lensDistortion:
                        effectSettings.enabled.value = PostProcessLensDistortion;
                        break;
                    case MotionBlur motionBlur:
                        effectSettings.enabled.value = PostProcessMotionBlur;
                        break;
                    case Vignette vignette:
                        effectSettings.enabled.value = PostProcessVignette;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(effectSettings));
                }
            }
        }
    }

    public static class VideoSettingsAttributes
    {
        
    }
}