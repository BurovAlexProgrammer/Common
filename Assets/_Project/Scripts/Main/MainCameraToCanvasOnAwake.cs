using _Project.Scripts.Main;
using UnityEngine;

public class MainCameraToCanvasOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Game.MainCamera;
    }
}
