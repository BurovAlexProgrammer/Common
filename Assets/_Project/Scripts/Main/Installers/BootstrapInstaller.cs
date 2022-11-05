using _Project.Scripts.Main.Services;
using Zenject;

namespace _Project.Scripts.Main.Installers
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
