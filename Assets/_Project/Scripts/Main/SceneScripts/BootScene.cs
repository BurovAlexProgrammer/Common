using _Project.Scripts.Main.Services;
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
            _sceneLoader.LoadScene(_sceneLoader.MainMenuScene.scenePath);
        }
    }
}