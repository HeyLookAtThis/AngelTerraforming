using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CameraController _gameCamera;
    [SerializeField] private CameraController _menuCamera;

    public void ActivateGameCamera()
    {
        _menuCamera.Deactivate();
        _gameCamera.Activate();
    }

    public void ActivateMenuCamera()
    {
        _gameCamera.Deactivate();
        _menuCamera.Activate();
    }
}
