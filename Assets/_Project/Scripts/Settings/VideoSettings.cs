using UnityEngine;

namespace _Project.Scripts.Settings
{
    [CreateAssetMenu(menuName = "Custom/Settings/Video")]
    public class VideoSettings: Settings
    {
        [SerializeField] private bool _good;
        public bool Good => _good;
    }
}