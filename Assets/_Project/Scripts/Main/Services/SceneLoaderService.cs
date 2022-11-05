using _Project.Scripts.Extension;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class SceneLoaderService: MonoBehaviour
    {
        [SerializeField] public ScenePicker MainMenuScene;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AnimationCurve _fadeCurve;

        private Scene _currentScene;
        private Scene _preparedScene;
        
        public void Init()
        {
            _canvasGroup.alpha = 1f;
        }

        //TODO to async unitask 
        public void LoadScene(string scenePath)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    HideScene();
                    PrepareScene(scenePath);
                })
                .AppendInterval(_fadeCurve.GetDuration())
                .AppendCallback(() =>
                {
                    ActivatePreparedScene();
                    ShowScene();
                });
        }

        private void ShowScene()
        {
            _canvasGroup
                .DOFade(0f, 0.5f).SetEase(_fadeCurve);
        }

        private void HideScene()
        {
            _canvasGroup
                .DOFade(1f, 0.3f).SetEase(_fadeCurve);
        }
        
        private void PrepareScene(string scenePath)
        {
            _currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
            _preparedScene = SceneManager.GetSceneByName(scenePath);
        }

        private void ActivatePreparedScene()
        {
            SceneManager.SetActiveScene(_preparedScene);
            SceneManager.UnloadSceneAsync(_currentScene);
        }
    }
}