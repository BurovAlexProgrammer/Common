using System;
using _Project.Scripts.Main.Services;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main
{
    public class GameState : MonoBehaviour
    {
        private GameStates _activeState;
        private SceneLoaderService _sceneLoader;

        public GameStates ActiveState => _activeState;

        [Inject]
        public void Construct(SceneLoaderService sceneLoaderService)
        {
            _sceneLoader = sceneLoaderService;
            EnterState(GameStates.Boot);
        }

        //TODO to Unitask
        public void SetState(GameStates newState)
        {
            //TODO DOTween.Sequence().AppendCallback()
            
            ExitState(_activeState);
            EnterState(newState);
        }

        private void ExitState(GameStates oldState)
        {
            Debug.Log("GameState ExitState: " + oldState, this);
            switch (oldState)
            {
                case GameStates.Boot:
                    ExitStateBoot();
                    break;
                case GameStates.MainMenu:
                    
                    break;
                case GameStates.PlayGame:
                    break;
                case GameStates.GamePause:
                    break;
            }
        }

        private void EnterState(GameStates newState)
        {
            Debug.Log("GameState Enter: " + newState, this);
            switch (newState)
            {
                case GameStates.Boot:
                    EnterStateBoot();
                    break;
                case GameStates.MainMenu:
                    EnterStateMainMenu();
                    break;
                case GameStates.PlayGame:
                    break;
                case GameStates.GamePause:
                    break;
            }
        }

        private void EnterStateBoot()
        {
            _sceneLoader.Init();
            DOTween.Sequence()
                .AppendInterval(1f) //TODO Show Intro
                .AppendCallback(() => SetState(GameStates.MainMenu));
        }
            
            
        private void ExitStateBoot()
        {
            
        }

        private void EnterStateMainMenu()
        {
            _sceneLoader.LoadScene(SceneLoaderService.Scenes.MainMenu);
        }
    }

    public enum GameStates
    {
        Boot, MainMenu, PlayGame, GamePause
    }
}