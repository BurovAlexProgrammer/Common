using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public static class Services
    {
        private static ScreenService _screenService;
        private static SceneLoaderService _sceneLoaderService;

        public static ScreenService ScreenService => _screenService;
        public static SceneLoaderService SceneLoaderService => _sceneLoaderService;


        public static void SetService<T>(T instance) where T : BaseService
        {
            switch (instance)
            {
                case ScreenService screenService:
                    Debug.Log("It is screenService");
                    _screenService = screenService;
                    break;
                case SceneLoaderService sceneLoaderService:
                    Debug.Log("It is SceneLoaderService");
                    _sceneLoaderService = sceneLoaderService;
                    break;
            }
        }
    }

    public abstract class BaseService : MonoBehaviour
    {
        
    }
}