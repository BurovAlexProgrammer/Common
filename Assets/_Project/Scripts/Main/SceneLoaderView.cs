using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main
{
    public class SceneLoaderView: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Init()
        {
            _canvasGroup.alpha = 1f;
        }
        
        public void Show()
        {
            _canvasGroup
                .DOFade(1f, 0.3f).SetEase(Ease.OutCubic);
        }

        public void Hide()
        {
            _canvasGroup
                .DOFade(0f, 0.3f).SetEase(Ease.OutCubic);
        }
    }
}