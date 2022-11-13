using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : MonoBehaviour
    {
        //TODO current game  state: MainMenu, Game etc;
        [SerializeField] private GameState _gameState;
        
        public GameState GameState => _gameState;
    }
}