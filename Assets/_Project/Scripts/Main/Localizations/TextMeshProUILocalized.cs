using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Localizations
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUILocalized : MonoBehaviour
    {
        [SerializeField] private string _localizedTextKey;

        private TextMeshProUGUI _textMesh;
        
        [Inject] private LocalizationService _localization;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private async void Start()
        {
            while (!_localization.IsLoaded)
            {
                await UniTask.NextFrame();
                Debug.Log("await");
            }

            Debug.Log("Done");
            _textMesh.text = _localization.GetLocalizedText(_localizedTextKey);
        }
    }
}