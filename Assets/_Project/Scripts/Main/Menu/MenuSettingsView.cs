using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Main.Menu
{
    public class MenuSettingsView : MonoBehaviour
    {
        [SerializeField] private MenuSettingsController _controller;
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonAdd;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _buttonAdd.onClick.AddListener(_controller.Add);
            _buttonApply.onClick.AddListener(_controller.Apply);
            _buttonReset.onClick.AddListener(_controller.ResetToDefault);
        }

        private void OnDestroy()
        {
            _buttonAdd.onClick.RemoveAllListeners();
            _buttonApply.onClick.RemoveAllListeners();
            _buttonReset.onClick.RemoveAllListeners();
        }
    }
}
