using _Project.Scripts.Main.Services;
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

        private static ScreenService _screenService;
        public static ScreenService ScreenService => _screenService;
        
        public override void InstallBindings()
        {
            InstallSceneLoaderService();
            InstallScreenService();
            InstallGameManagerService();
            InstallSettingService();
        }

        private void InstallSettingService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(_settingsServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var settingService = (SettingsService)instance;
                    settingService.Init();
                });
        }

        private void InstallScreenService()
        {
            Container
                .Bind<ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => _screenService = (ScreenService)instance)
                .NonLazy();
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
