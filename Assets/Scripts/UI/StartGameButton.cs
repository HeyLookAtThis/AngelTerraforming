using UnityEngine;

public class StartGameButton : MenuButton
{
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private CanvasChanger _canvasChanger;

    private void OnEnable()
    {
        AddAction(TurnOnGameCamera);
    }

    private void OnDisable()
    {
        RemoveAction(TurnOnGameCamera);
    }

    private void TurnOnGameCamera()
    {
        _cameraChanger.ActivateGameCamera();
        _canvasChanger.ActivateGameCamera();
    }
}
