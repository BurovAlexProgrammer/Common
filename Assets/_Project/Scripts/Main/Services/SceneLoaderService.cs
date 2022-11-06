using _Project.Scripts.Extension;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class SceneLoaderService: MonoBehaviour
    { 
        public ScenePicker MainMenuScene;
        [SerializeField] private CanvasGroup _blackFrame;
        [SerializeField] private AnimationCurve _fadeCurve;

        private Scene _currentScene;
        private Scene _preparedScene;
        
        public void Init()
        {
            _blackFrame.alpha = 1f;
        }

        //TODO to async unitask 
        public void LoadScene(string scenePath)
        {
            Debug.Log(_fadeCurve.GetDuration());
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
            _blackFrame.gameObject.SetActive(true);
            _blackFrame
                .DOFade(0f, 0.5f)
                .From(1f)
                .SetEase(_fadeCurve)
                .OnComplete(() => _blackFrame.gameObject.SetActive(false));
        }

        private void HideScene()
        {
            _blackFrame.gameObject.SetActive(true);
            _blackFrame
                .DOFade(1f, 0.3f)
                .From(0f)
                .SetEase(_fadeCurve)
                .OnComplete(() => _blackFrame.gameObject.SetActive(false));
        }
        
        private void PrepareScene(string scenePath)
        {
            _currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
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