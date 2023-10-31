using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private CameraController _gameCamera;
    [SerializeField] private CameraController _menuCamera;
    [SerializeField] private LevelFinisher _levelFinisher;
    [SerializeField] private LevelStarter _levelStarter;

    private void OnEnable()
    {
        _levelFinisher.Begun += ActivateMenuCamera;
        _levelStarter.Beginning += ActivateGameCamera;
    }

    private void OnDisable()
    {
        _levelFinisher.Begun -= ActivateMenuCamera;
        _levelStarter.Beginning -= ActivateGameCamera;
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
