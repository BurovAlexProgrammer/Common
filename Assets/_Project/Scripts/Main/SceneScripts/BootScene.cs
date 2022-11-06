using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.SceneScripts
{
    public class BootScene : MonoBehaviour
    {
        private SceneLoaderService _sceneLoader;
        
        [Inject]
        public void Construct(SceneLoaderService sceneLoaderService)
        {
            _sceneLoader = sceneLoaderService;
        }
        
        private void Start()
        {
            _sceneLoader.Init();
            DOTween.Sequence()
                .AppendInterval(1)
                .AppendCallback(() => _sceneLoader.LoadScene(_sceneLoader.MainMenuScene));
        }
    }
}