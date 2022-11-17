using _Project.Scripts.Main.Services;
using _Project.Scripts.Settings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoaderServicePrefab;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private GameManagerService _gameManagerServicePrefab;

        public override void InstallBindings()
        {
            InstallSceneLoaderService();
            InstallScreenService();
            InstallSettingService();
            InstallGameManagerService();
        }

        private void InstallSettingService()
        {
            var settingsService = Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(_settingsServicePrefab)
                .AsSingle();
            settingsService.OnInstantiated((ctx, obj) => ((SettingsService)obj).Init());
        }

        private void InstallScreenService()
        {
            Container
                .Bind<ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .AsSingle();
        }

        private void InstallSceneLoaderService()
        {
            Container
                .Bind<SceneLoaderService>()
                .FromComponentInNewPrefab(_sceneLoaderServicePrefab)
                .AsSingle();
        }

        private void InstallGameManagerService()
        {
            Container
                .Bind<GameManagerService>()
                .FromComponentInNewPrefab(_gameManagerServicePrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}
