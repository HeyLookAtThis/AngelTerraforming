using UnityEngine;

public class StartGameButton : MenuButton
{
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private CanvasChanger _canvasChanger;
    [SerializeField] private LevelGenerator _levelGenerator;

    private void OnEnable()
    {
        AddAction(_levelGenerator.SetNextLevel);
        AddAction(_levelGenerator.GenerateLevel);
        AddAction(TurnOnGameCamera);
    }

    private void OnDisable()
    {
        RemoveAction(_levelGenerator.SetNextLevel);
        RemoveAction(_levelGenerator.GenerateLevel);
        RemoveAction(TurnOnGameCamera);
    }

    private void TurnOnGameCamera()
    {
        _cameraChanger.ActivateGameCamera();
        _canvasChanger.ActivateGameCamera();
    }
}
