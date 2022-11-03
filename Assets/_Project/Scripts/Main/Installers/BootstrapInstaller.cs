using _Project.Scripts.Main;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Common.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public SceneLoaderService _sceneLoaderServicePrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<SceneLoaderService>()
                .FromComponentInNewPrefab(_sceneLoaderServicePrefab)
                .AsSingle();
        }
    }
}
