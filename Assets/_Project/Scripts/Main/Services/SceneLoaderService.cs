using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class SceneLoaderService: MonoBehaviour
    { 
        [SerializeField] private ScenePicker _mainMenuScene;
        [SerializeField] private CanvasGroup _blackFrame;
        [SerializeField] private AnimationCurve _fadeCurve;
        
        public string MainMenuScene => _mainMenuScene.scenePath;
        private Scene _currentScene;
        private Scene _preparedScene;
        
        public void Init()
        {
            _blackFrame.alpha = 1f;
            ShowScene();
        }
        
        public void LoadScene(string scenePath)
        {
            LoadSceneAsync(scenePath).Forget();
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

        private async UniTask HideScene()
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
    }
}