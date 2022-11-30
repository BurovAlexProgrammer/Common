using System;
using System.Collections.Generic;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class SceneLoaderService : MonoBehaviour
    {
        [SerializeField] private ScenePicker _mainMenuScene;
        [SerializeField] private CanvasGroup _blackFrame;
        [SerializeField] private AnimationCurve _fadeCurve;

        private Scene _currentScene;
        private Scene _preparedScene;
        private Scene _initialScene;

        public string MainMenuScene => _mainMenuScene.scenePath;
        public Scene InitialScene => _initialScene;

        public void Init()
        {
            _initialScene = SceneManager.GetActiveScene();
            _blackFrame.alpha = 1f;
            ShowScene();
        }

        public void LoadScene(Scenes scene)
        {
            var sceneName = GetScene(scene);
            LoadSceneAsync(sceneName).Forget();
        }

        private async UniTask LoadSceneAsync(string scenePath)
        {
            await UniTask.WhenAll(HideScene(), PrepareScene(scenePath));
            ActivatePreparedScene();
            ShowScene();
        }

        private void ShowScene()
        {
            _blackFrame.gameObject.SetActive(true);
            _blackFrame
                .DOFade(0f, 0.5f)
                .From(1f)
                .SetEase(_fadeCurve)
                .OnComplete(() => _blackFrame.gameObject.SetActive(false));
        }

        public async UniTask HideScene()
        {
            _blackFrame.gameObject.SetActive(true);
            await _blackFrame
                .DOFade(1f, 0.3f)
                .From(0f)
                .AsyncWaitForCompletion();
        }

        private async UniTask PrepareScene(string scenePath)
        {
            _currentScene = SceneManager.GetActiveScene();
            await SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            _preparedScene = SceneManager.GetSceneByName(scenePath);
            _preparedScene.SetActive(false);
        }

        private void ActivatePreparedScene()
        {
            _preparedScene.SetActive(true);
            SceneManager.SetActiveScene(_preparedScene);
            SceneManager.UnloadSceneAsync(_currentScene);
        }

        public enum Scenes
        {
            Boot,
            MainMenu,
            IntroLevel,
        }

        private string GetSceneName(Scenes scene)
        {
            return SceneNames[scene];
        }

        public bool InitialSceneEquals(Scenes scene)
        {
            return GetSceneName(scene).Equals(_initialScene.name);
        }

        private Dictionary<Scenes, string> SceneNames = new Dictionary<Scenes, string>()
        {
            { Scenes.Boot, "Boot" },
            { Scenes.MainMenu, "MainMenu" },
        };

        private string GetScene(Scenes scene)
        {
            if (SceneNames.ContainsKey(scene)) return SceneNames[scene];

            throw new Exception("Scene Key not found!");
        }
    }
}