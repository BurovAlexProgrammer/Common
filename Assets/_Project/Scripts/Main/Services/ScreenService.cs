using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace _Project.Scripts.Main.Services
{
    public class ScreenService : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PostProcessVolume _postProcessVolume;
        [SerializeField] private PostProcessLayer _postProcessLayer;

        public Camera MainCamera => _mainCamera;
    }
}