using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CameraController _gameCamera;
    [SerializeField] private CameraController _menuCamera;
    [SerializeField] private VolcanoDisplayer _volcanoDisplayer;

    private void OnEnable()
    {
        _volcanoDisplayer.Fulled += ActivateMenuCamera;
    }

    private void OnDisable()
    {
        _volcanoDisplayer.Fulled -= ActivateMenuCamera;
    }

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
