using UnityEngine;

namespace _Project.Scripts.Main
{
    public static class Game
    {
        private static Camera _mainCamera;
        public static Camera MainCamera => _mainCamera ?? Camera.main;
    }
}