using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : MonoBehaviour
    {
        [SerializeField] private GameStateMachine _gameStateMachine;
        
        public GameStateMachine GameStateMachine => _gameStateMachine;
    }
}