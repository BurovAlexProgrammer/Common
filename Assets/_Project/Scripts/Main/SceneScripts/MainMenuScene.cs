using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.SceneScripts
{
    public class MainMenuScene : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;

        void Start()
        {
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
        }

        private void OnSettingsButtonClicked()
        {
            Debug.Log("Settings!");
        }
    }
}
