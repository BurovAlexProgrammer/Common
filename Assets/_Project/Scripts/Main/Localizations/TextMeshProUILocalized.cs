using _Project.Scripts.Main.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Localizations
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUILocalized : LocalizedTextComponent
    {
        [SerializeField] private string _localizedTextKey;

        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        protected override void SetText()
        {
            _textMesh.text = _localization.GetLocalizedText(_localizedTextKey);
        }
    }
}