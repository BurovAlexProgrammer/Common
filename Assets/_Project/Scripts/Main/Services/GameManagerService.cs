using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;
        
        public GameState GameState => _gameState;
    }
}