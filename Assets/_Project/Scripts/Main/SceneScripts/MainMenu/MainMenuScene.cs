using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.SceneScripts.MainMenu
{
    public class MainMenuScene : MonoBehaviour
    {
        [SerializeField] private MainMenuStateMachine _menuStateMachine;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backFromSettings;

        void Start()
        {
            _settingsButton.onClick.AddListener(() => _menuStateMachine.SetState(MenuStates.Settings));
            _backFromSettings.onClick.AddListener(() => _menuStateMachine.SetState(MenuStates.MainMenu));
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
        }
    }
}
