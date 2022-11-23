using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsView : MonoBehaviour
    {
        [SerializeField] private MenuSettingsController _controller;
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private VideoSettingViews _videoSettingViews;

        private void Awake()
        {
            _buttonApply.onClick.AddListener(_controller.Apply);
            _buttonReset.onClick.AddListener(ResetToDefault);
            _videoSettingViews.AntiAliasing.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessAntiAliasing));
            
            _videoSettingViews.Bloom.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessBloom));
            
            _videoSettingViews.Vignette.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessVignette));
            
            _videoSettingViews.AmbientOcclusion.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessAmbientOcclusion));
            
            _videoSettingViews.DepthOfField.onValueChanged.AddListener(
                value => _controller.Bind(value, ref _controller.VideoSettings.PostProcessDepthOfField));
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _videoSettingViews.AntiAliasing.isOn = _controller.VideoSettings.PostProcessAntiAliasing;
            _videoSettingViews.Bloom.isOn = _controller.VideoSettings.PostProcessBloom;
            _videoSettingViews.Vignette.isOn = _controller.VideoSettings.PostProcessVignette;
            _videoSettingViews.AmbientOcclusion.isOn = _controller.VideoSettings.PostProcessAmbientOcclusion;
            _videoSettingViews.DepthOfField.isOn = _controller.VideoSettings.PostProcessDepthOfField;
        }
        
        private void OnDestroy()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonReset.onClick.RemoveAllListeners();
            _videoSettingViews.AntiAliasing.onValueChanged.RemoveAllListeners();
            _videoSettingViews.Bloom.onValueChanged.RemoveAllListeners();
            _videoSettingViews.Vignette.onValueChanged.RemoveAllListeners();
            _videoSettingViews.AmbientOcclusion.onValueChanged.RemoveAllListeners();
            _videoSettingViews.DepthOfField.onValueChanged.RemoveAllListeners();
        }

        private void ResetToDefault()
        {
            _controller.ResetToDefault();
            Init();
        }
        
        [Serializable]
        private class VideoSettingViews
        {
            public Toggle AntiAliasing;
            public Toggle Bloom;
            public Toggle Vignette;
            public Toggle AmbientOcclusion;
            public Toggle DepthOfField;
        }
    }
}
