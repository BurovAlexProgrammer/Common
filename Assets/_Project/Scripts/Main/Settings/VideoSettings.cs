using System;
using _Project.Scripts.Main.Installers;
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
        
        private PostProcessLayer _postProcessLayer;
        private PostProcessVolume _postProcessVolume;

        [Inject]
        public void Construct(ScreenService screenService)
        {
            //TODO Research. Inject does not call, dont know why.
        }

        //Crutch instead of injection
        public override void Init()
        {
            _postProcessLayer = BootstrapInstaller.ScreenService.PostProcessLayer;
            _postProcessVolume = BootstrapInstaller.ScreenService.PostProcessVolume;
        }

        public override void ApplySettings()
        {
            _postProcessLayer.antialiasingMode = PostProcessAntiAliasing ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : None;
            foreach (var effectSettings in _postProcessVolume.sharedProfile.settings)
            {
                switch (effectSettings)
                {
                    case AmbientOcclusion:
                        effectSettings.enabled.value = PostProcessAmbientOcclusion;
                        break;
                    case Bloom:
                        effectSettings.enabled.value = PostProcessBloom;
                        break;
                    case ChromaticAberration:
                        effectSettings.enabled.value = PostProcessChromaticAberration;
                        break;
                    case DepthOfField:
                        effectSettings.enabled.value = PostProcessDepthOfField;
                        break;
                    case LensDistortion:
                        effectSettings.enabled.value = PostProcessLensDistortion;
                        break;
                    case MotionBlur:
                        effectSettings.enabled.value = PostProcessMotionBlur;
                        break;
                    case Vignette:
                        effectSettings.enabled.value = PostProcessVignette;
                        break;
                    case ColorGrading:
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