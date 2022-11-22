using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : MonoBehaviour
    {
        [SerializeField] private GameStateMachine _gameStateMachine;
        
        public GameStateMachine GameStateMachine => _gameStateMachine;

        public void QuitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else 
            Application.Quit();
            #endif
        }
    }
}