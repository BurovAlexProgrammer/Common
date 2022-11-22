using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using _Project.Scripts.Main.Services;
using static _Project.Scripts.Extension.Common;

namespace _Project.Scripts.Main
{
    public class GameStateMachine : MonoBehaviour
    {
        private GameStates _activeState;
        private SceneLoaderService _sceneLoader;

        public Action StateChanged;

        public GameStates ActiveState => _activeState;

        [Inject]
        public void Construct(SceneLoaderService sceneLoaderService)
        {
            _sceneLoader = sceneLoaderService;
            _ = EnterState(GameStates.Boot);
        }

        public async void SetState(GameStates newState)
        {
            await ExitState(_activeState);
            await EnterState(newState);
            StateChanged?.Invoke();
        }

        private async UniTask EnterState(GameStates newState)
        {
            Debug.Log("GameState Enter: " + newState, this);
            switch (newState)
            {
                case GameStates.Boot:
                    await EnterStateBoot();
                    break;
                case GameStates.MainMenu:
                    EnterStateMainMenu();
                    break;
                case GameStates.PlayGame:
                    break;
                case GameStates.GamePause:
                    break;
                case GameStates.GameQuit:
                    
                    break;
                default:
                    throw new Exception("GameManager: unknown state.");
            }
        }
        
        private async UniTask ExitState(GameStates oldState)
        {
            Debug.Log("GameState ExitState: " + oldState, this);
            switch (oldState)
            {
                case GameStates.Boot:
                    await ExitStateBoot();
                    break;
                case GameStates.MainMenu:
                    break;
                case GameStates.PlayGame:
                    break;
                case GameStates.GamePause:
                    break;
                default:
                    throw new Exception("GameManager: unknown state.");
            }
        }

        private async UniTask EnterStateBoot()
        {
            _sceneLoader.Init();
            await Wait(1f);
            SetState(GameStates.MainMenu);
        }

        private async UniTask ExitStateBoot()
        {
            await UniTask.Delay(1);
        }

        private void EnterStateMainMenu()
        {
            _sceneLoader.LoadScene(SceneLoaderService.Scenes.MainMenu);
        }
    }

    public enum GameStates
    {
        Boot, MainMenu, PlayGame, GamePause, GameQuit
    }
}