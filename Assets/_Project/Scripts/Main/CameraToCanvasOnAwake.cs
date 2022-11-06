using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

public class CameraToCanvasOnAwake : MonoBehaviour
{
    [SerializeField] private CameraTypes _camera;

    private enum CameraTypes
    {
        MainCamera,
        UiCamera
    }
    
    [Inject]
    public void Construct(ScreenService screenService)
    {
        GetComponent<Canvas>().worldCamera =
            _camera == CameraTypes.MainCamera ? screenService.MainCamera : screenService.UICamera;
        enabled = false;
    }

    private void OnDisable() {}
}
