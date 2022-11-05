using _Project.Scripts.Main;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;

public class MainCameraToCanvasOnAwake : MonoBehaviour
{
    [Inject]
    public void Construct(ScreenService screenService)
    {
        GetComponent<Canvas>().worldCamera = screenService.MainCamera;
        gameObject.SetActive(false);
    }
}
