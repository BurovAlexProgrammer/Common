using UnityEngine;
// ReSharper disable InconsistentNaming

namespace _Project.Scripts.Main
{
    public abstract class MonoWrapper : MonoBehaviour
    {
        private new GameObject gameObject;
        private new Transform transform;
        public Transform _transform;
        public GameObject _gameObject;

        public virtual void Awake()
        {
            _transform = base.transform;
            _gameObject = base.gameObject;
        }
    }
}